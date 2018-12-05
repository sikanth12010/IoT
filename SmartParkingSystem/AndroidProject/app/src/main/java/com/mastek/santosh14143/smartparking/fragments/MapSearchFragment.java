package com.mastek.santosh14143.smartparking.fragments;

import android.Manifest;
import android.app.AlertDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.content.pm.PackageManager;
import android.graphics.Bitmap;
import android.graphics.Canvas;
import android.graphics.drawable.Drawable;
import android.location.Location;
import android.location.LocationListener;
import android.location.LocationManager;
import android.os.Bundle;
import android.provider.Settings;
import android.support.annotation.DrawableRes;
import android.support.annotation.Nullable;
import android.support.v4.app.ActivityCompat;
import android.support.v4.app.Fragment;
import android.support.v4.content.ContextCompat;
import android.util.Log;
import android.view.InflateException;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;

import com.google.android.gms.common.api.Status;
import com.google.android.gms.location.places.Place;
import com.google.android.gms.location.places.ui.PlaceAutocompleteFragment;
import com.google.android.gms.location.places.ui.PlaceSelectionListener;
import com.google.android.gms.maps.CameraUpdate;
import com.google.android.gms.maps.CameraUpdateFactory;
import com.google.android.gms.maps.GoogleMap;
import com.google.android.gms.maps.MapFragment;
import com.google.android.gms.maps.OnMapReadyCallback;
import com.google.android.gms.maps.model.BitmapDescriptor;
import com.google.android.gms.maps.model.BitmapDescriptorFactory;
import com.google.android.gms.maps.model.LatLng;
import com.google.android.gms.maps.model.LatLngBounds;
import com.google.android.gms.maps.model.Marker;
import com.google.android.gms.maps.model.MarkerOptions;
import com.google.maps.android.ui.IconGenerator;
import com.mastek.santosh14143.smartparking.R;
import com.mastek.santosh14143.smartparking.activity.DashBoardActivity;
import com.mastek.santosh14143.smartparking.retrofit.APIClient;
import com.mastek.santosh14143.smartparking.retrofit.APIInterface;
import com.mastek.santosh14143.smartparking.retrofit.response.LocationResponse;
import com.mastek.santosh14143.smartparking.util.Constants;
import com.mastek.santosh14143.smartparking.util.Utility;

import java.util.Hashtable;
import java.util.List;
import java.util.Map;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

import static android.content.Context.LOCATION_SERVICE;

/**
 * Created by santosh14143 on 4/16/2018.
 */public class MapSearchFragment extends Fragment implements OnMapReadyCallback, LocationListener, GoogleMap.OnMarkerClickListener {
    View view;
    private GoogleMap googleMap;
    private LocationManager locationManager;
    private Marker marker;
    private PlaceAutocompleteFragment placeAutoComplete;
    private LatLngBounds.Builder builder = new LatLngBounds.Builder();
//    private int reduceParking;
    IconGenerator generator;

    @Nullable
        @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        if (view != null) {
            ViewGroup parent = (ViewGroup) view.getParent();
            if (parent != null)
                parent.removeView(view);
        }
        try {
            view = inflater.inflate(R.layout.dashboard_layout, container, false);
        }catch (InflateException e){
            e.printStackTrace();
        }

      /*  Bundle bundle = getArguments();
        if(bundle == null){
            reduceParking = 0;
        }else{
            reduceParking = bundle.getInt("reduce");
        }*/

        generator = new IconGenerator(this.getContext());
//        generator.setContentRotation(270);
        generator.setStyle(IconGenerator.STYLE_BLUE);

        placeAutoComplete = (PlaceAutocompleteFragment) getActivity().getFragmentManager().findFragmentById(R.id.place_autocomplete);
        placeAutoComplete.setOnPlaceSelectedListener(new PlaceSelectionListener() {
            @Override
            public void onPlaceSelected(Place place) {

                Log.d("Maps", "Place selected: " + place.getName());
                marker.setPosition(place.getLatLng());
                googleMap.moveCamera(CameraUpdateFactory.newLatLngZoom(place.getLatLng(), 19));
            }

            @Override
            public void onError(Status status) {
                Log.d("Maps", "An error occurred: " + status);
            }
        });

        MapFragment mapFragment = (MapFragment) getActivity().getFragmentManager().findFragmentById(R.id.map);
        mapFragment.getMapAsync(this);

        locationManager = (LocationManager) getActivity().getSystemService(LOCATION_SERVICE);

        if (ActivityCompat.checkSelfPermission(this.getActivity(), Manifest.permission.ACCESS_FINE_LOCATION) != PackageManager.PERMISSION_GRANTED && ActivityCompat.checkSelfPermission(this.getActivity(), Manifest.permission.ACCESS_COARSE_LOCATION) != PackageManager.PERMISSION_GRANTED) {
            return null;
        }
        locationManager.requestLocationUpdates(LocationManager.GPS_PROVIDER, 1000, 10, this);
        locationManager.requestLocationUpdates(LocationManager.NETWORK_PROVIDER, 1000, 10, this);
        return view;
    }

    @Override
    public void onLocationChanged(Location location) {
        Log.e(Constants.TAG,"INside onLocationChanged");
        Double latitude = location.getLatitude();
        Double longitude = location.getLongitude();
        LatLng myLoc = new LatLng(latitude,longitude);
//        googleMap.addMarker(new MarkerOptions().position(myLoc).title("My Location"));
        marker.setPosition(myLoc);
        updateMapBounds(marker);
//        googleMap.moveCamera(CameraUpdateFactory.newLatLngZoom(myLoc,19));

    }

    @Override
    public void onStatusChanged(String s, int i, Bundle bundle) {

    }

    @Override
    public void onProviderEnabled(String s) {

    }

    @Override
    public void onProviderDisabled(String s) {

    }

    @Override
    public void onMapReady(GoogleMap googleMap) {
        this.googleMap = googleMap;
        this.googleMap.setOnMarkerClickListener(this);

        LatLng sydney = new LatLng(-34, 151);
        this.googleMap.clear();
        marker = googleMap.addMarker(new MarkerOptions().position(sydney).title("Marker in Sydney"));
//        googleMap.moveCamera(CameraUpdateFactory.newLatLng(sydney));

        getParkings("19.0693219","72.8915444");


    }

    public void updateMapBounds(Marker marker){
        builder.include(marker.getPosition());
        LatLngBounds bounds = builder.build();
        CameraUpdate cu = CameraUpdateFactory.newLatLngBounds(bounds, 50);
        googleMap.animateCamera(cu);
    }



    private void isLocationEnabled() {

        if(!locationManager.isProviderEnabled(LocationManager.GPS_PROVIDER)){
            AlertDialog.Builder alertDialog=new AlertDialog.Builder(this.getActivity());
            alertDialog.setTitle("Enable Location");
            alertDialog.setMessage("Your locations setting is not enabled. Please enabled it in settings menu.");
            alertDialog.setPositiveButton("Location Settings", new DialogInterface.OnClickListener(){
                public void onClick(DialogInterface dialog, int which){
                    Intent intent=new Intent(Settings.ACTION_LOCATION_SOURCE_SETTINGS);
                    startActivity(intent);
                }
            });
            alertDialog.setNegativeButton("Cancel", new DialogInterface.OnClickListener(){
                public void onClick(DialogInterface dialog, int which){
                    dialog.cancel();
                }
            });
            AlertDialog alert=alertDialog.create();
            alert.show();
        }
        else{
            AlertDialog.Builder alertDialog=new AlertDialog.Builder(this.getActivity());
            alertDialog.setTitle("Confirm Location");
            alertDialog.setMessage("Your Location is enabled, please enjoy");
            alertDialog.setNegativeButton("Back to interface",new DialogInterface.OnClickListener(){
                public void onClick(DialogInterface dialog, int which){
                    dialog.cancel();
                }
            });
            AlertDialog alert=alertDialog.create();
            alert.show();
        }
    }

    @Override
    public boolean onMarkerClick(Marker marker) {
        Object obj = marker.getTag();
        if(obj == null){
            Log.e(Constants.TAG, "MARKER CLICKED : CURRENT LOCATION");
        }else{
            LocationResponse locationResponse = (LocationResponse)obj;
            Log.e(Constants.TAG, "MARKER CLICKED : "+locationResponse.getName());
            Bundle bundle = new Bundle();
            bundle.putParcelable(Constants.PARCELABLE,locationResponse);
//            bundle.putInt("reduceVal",reduceParking);
            ((DashBoardActivity)getActivity()).switchFragment(Constants.BOOKING_FRAG,bundle);
        }

        return false;
    }

    private void getParkings(String latitude, String longitude){
        Utility.showProgressDialog(this.getActivity(),"Loading...", "Please wait");

        APIInterface apiInterface = APIClient.getClient().create(APIInterface.class);
        Map<String, String> requestMap = new Hashtable<>();
        requestMap.put("lat",latitude);
        requestMap.put("lng",longitude);
        Call<List<LocationResponse>> call = apiInterface.getParkings(requestMap);
        call.enqueue(new Callback<List<LocationResponse>>() {
            @Override
            public void onResponse(Call<List<LocationResponse>> call, Response<List<LocationResponse>> response) {
                Utility.closeProgressDialog();
                Log.e(Constants.TAG,"response : "+response);
                if(response.code() == 200){

                    try {
                        List<LocationResponse> responseBody = response.body();
                        for(int i=0;i<responseBody.size();i++){
                            LocationResponse locationResponse = responseBody.get(i);
                            Log.e(Constants.TAG,"responseBody : "+locationResponse);
                            locationResponse.setAspaces(locationResponse.getAspaces()/*-reduceParking*/);

                            LocationResponse.Loc loc = locationResponse.getLoc();
                            List<Double> coordinates = loc.getCoordinates();
                            Log.e(Constants.TAG,"lat  : "+coordinates.get(0)+", lng : "+coordinates.get(1));


                            Marker pMarker= googleMap.addMarker(new MarkerOptions().position(new LatLng(coordinates.get(0),coordinates.get(1)))
                                    .icon(BitmapDescriptorFactory.fromBitmap(generator.makeIcon(""+locationResponse.getAspaces())))
                                    .anchor(generator.getAnchorU(), generator.getAnchorV()));
//                                .icon(BitmapDescriptorFactory.fromResource(R.drawable.parking)));
//                                    .icon(bitmapDescriptorFromVector(MapSearchFragment.this.getContext(),R.drawable.ic_local_parking_black_24dp)));
//                            pMarker.setTitle("Available : "+locationResponse.getAspaces());
                            pMarker.setTag(locationResponse);
//                            pMarker.showInfoWindow();

                            updateMapBounds(pMarker);
                        }


                    }catch (Exception e){
                        e.printStackTrace();
                    }
                    /*alert.setMessage("loginResponse : "+loginResponse);
                    alert.show();*/
                }else{
                    Log.e(Constants.TAG,"Error occurred with response code : "+response.code());
                    /*Snackbar snackbar = Snackbar.make(rootView, "Error occurred with response code : "+response.code(), Snackbar.LENGTH_SHORT);
                    snackbar.show();*/
                }

            }

            @Override
            public void onFailure(Call<List<LocationResponse>> call, Throwable t) {
                call.cancel();
                Utility.closeProgressDialog();
                Log.e(Constants.TAG,"Request Failed!"+t.getLocalizedMessage());
               /* Snackbar snackbar = Snackbar.make(rootView, "Request Failed!", Snackbar.LENGTH_SHORT);
                snackbar.show();*/
            }
        });

    }

    private BitmapDescriptor bitmapDescriptorFromVector(Context context, @DrawableRes int vectorDrawableResourceId) {
        Drawable background = ContextCompat.getDrawable(context, R.drawable.ic_local_parking_black_24dp);
        background.setBounds(0, 0, background.getIntrinsicWidth(), background.getIntrinsicHeight());
        Drawable vectorDrawable = ContextCompat.getDrawable(context, vectorDrawableResourceId);
        vectorDrawable.setBounds(40, 20, vectorDrawable.getIntrinsicWidth() + 40, vectorDrawable.getIntrinsicHeight() + 20);
        Bitmap bitmap = Bitmap.createBitmap(background.getIntrinsicWidth(), background.getIntrinsicHeight(), Bitmap.Config.ARGB_8888);
        Canvas canvas = new Canvas(bitmap);
        background.draw(canvas);
        vectorDrawable.draw(canvas);
        return BitmapDescriptorFactory.fromBitmap(bitmap);
    }

    @Override
    public void onPause() {
        super.onPause();
        locationManager.removeUpdates(this);
    }
}
