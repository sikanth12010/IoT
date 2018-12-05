package com.mastek.santosh14143.smartparking.fragments;

import android.app.AlertDialog;
import android.app.NotificationManager;
import android.content.DialogInterface;
import android.media.RingtoneManager;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.design.widget.CoordinatorLayout;
import android.support.design.widget.Snackbar;
import android.support.design.widget.TextInputEditText;
import android.support.v4.app.Fragment;
import android.support.v4.app.NotificationCompat;
import android.text.Editable;
import android.text.TextWatcher;
import android.util.Log;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.Button;

import com.google.android.gms.maps.model.BitmapDescriptorFactory;
import com.google.android.gms.maps.model.LatLng;
import com.google.android.gms.maps.model.Marker;
import com.google.android.gms.maps.model.MarkerOptions;
import com.google.gson.Gson;
import com.mastek.santosh14143.smartparking.R;
import com.mastek.santosh14143.smartparking.activity.DashBoardActivity;
import com.mastek.santosh14143.smartparking.model.User;
import com.mastek.santosh14143.smartparking.retrofit.APIClient;
import com.mastek.santosh14143.smartparking.retrofit.APIInterface;
import com.mastek.santosh14143.smartparking.retrofit.request.BookingRequest;
import com.mastek.santosh14143.smartparking.retrofit.response.LocationResponse;
import com.mastek.santosh14143.smartparking.util.Constants;
import com.mastek.santosh14143.smartparking.util.Utility;

import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.Hashtable;
import java.util.List;
import java.util.Map;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

import static android.content.Context.NOTIFICATION_SERVICE;

public class BookingFragment extends Fragment {

    View bookingView;
    CoordinatorLayout coordinator;
    TextInputEditText dateET;
    TextInputEditText hoursET;
    TextInputEditText amountET;
    TextInputEditText addressET;
    TextInputEditText vehNoET;
    Button bookBtn;
    SimpleDateFormat simpleDateFormat = new SimpleDateFormat("dd/MM/yyyy HH:mm");
    AlertDialog.Builder bookingAlert;
    Date date;
    int reduceVal;
    LocationResponse locationResponse;
    Utility utility;
    User user;

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        bookingView = inflater.inflate(R.layout.confirm_booking_layout,container,false);
        Bundle bundle = getArguments();
        locationResponse = bundle.getParcelable(Constants.PARCELABLE);
        reduceVal = bundle.getInt("reduceVal");
        Log.e(Constants.TAG,"reduceVal : "+reduceVal);

        utility = Utility.getInsatnce(this.getActivity());
        user = (User)utility.convertStringToObject(utility.getStringPreference(Constants.USER),User.class);

        coordinator = bookingView.findViewById(R.id.booking_coordinator);
        dateET = bookingView.findViewById(R.id.booking_date);
        hoursET = bookingView.findViewById(R.id.booking_hours);
        amountET = bookingView.findViewById(R.id.booking_total_amt);
        addressET = bookingView.findViewById(R.id.booking_address);
        vehNoET = bookingView.findViewById(R.id.booking_vehicle_no);
        bookBtn = bookingView.findViewById(R.id.bookConfirmBtn);

        date = new Date();
        dateET.setText(simpleDateFormat.format(date));
        addressET.setText(locationResponse.getName());
        hoursET.setText("0");
        amountET.setText(""+(Integer.parseInt(hoursET.getText().toString())*10));

        hoursET.addTextChangedListener(new TextWatcher() {
            @Override
            public void beforeTextChanged(CharSequence charSequence, int i, int i1, int i2) {

            }

            @Override
            public void onTextChanged(CharSequence charSequence, int i, int i1, int i2) {
                String hrsTxt = hoursET.getText().toString();
                if(!hrsTxt.equals("")){
                    amountET.setText("Rs. "+(Integer.parseInt(hrsTxt)*10));
                }else{
                    amountET.setText("Rs. 0");
                }

            }

            @Override
            public void afterTextChanged(Editable editable) {

            }
        });

        bookingAlert = new AlertDialog.Builder(getContext())
        .setTitle("Booking Confirmed!")
        .setPositiveButton("OK", new DialogInterface.OnClickListener() {
            @Override
            public void onClick(DialogInterface dialogInterface, int i) {
//                Bundle bundle = new Bundle();
//                reduceVal++;
//                bundle.putInt("reduce",reduceVal);
                ((DashBoardActivity)getActivity()).switchFragment(Constants.MAP_SEARCH_FRAG,null);
            }
        });

        bookBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                if(!vehNoET.getText().toString().trim().equals("")){
                    bookParkingSlot();
                }else{
                    Snackbar.make(coordinator,"Enter Vehicle no!",Snackbar.LENGTH_SHORT).show();
                }
                /*bookingAlert.setMessage("Booking confirmed for Vehicle no. MH 04 HG 2797 at "+simpleDateFormat.format(date))
                .show();
                createNotification();*/
            }
        });
        return bookingView;
    }

    public void createNotification(){
        NotificationCompat.Builder mBuilder = new NotificationCompat.Builder(this.getContext(),"")
                .setSmallIcon(R.mipmap.ic_launcher_round)
                .setContentTitle("Parking Booked")
                .setContentText("Booking confirmed for Vehicle no. MH 04 HG 2797 at "+simpleDateFormat.format(date))
                .setPriority(NotificationCompat.PRIORITY_DEFAULT)
                .setSound(RingtoneManager.getDefaultUri(RingtoneManager.TYPE_NOTIFICATION));

        NotificationManager notificationManager = (NotificationManager) getActivity().getSystemService(NOTIFICATION_SERVICE);
        notificationManager.notify(0,mBuilder.build());
    }

    private void bookParkingSlot(){
        Utility.showProgressDialog(this.getActivity(),"Loading...", "Please wait");

        APIInterface apiInterface = APIClient.getClient().create(APIInterface.class);

        BookingRequest request = new BookingRequest();
        request.setCar_park_id(locationResponse.get_id());
        request.setCust_id(user.getId());
        request.setVehicle_no(vehNoET.getText().toString().trim());

//        Map<String, String> requestMap = new Hashtable<>();
//        requestMap.put("car_park_id",locationResponse.get_id());
//        requestMap.put("cust_id",user.getId());
//        requestMap.put("vehicle_no",vehNoET.getText().toString().trim());
        Call<String> call = apiInterface.doBooking(request);
        call.enqueue(new Callback<String>() {
            @Override
            public void onResponse(Call<String> call, Response<String> response) {
               Utility.closeProgressDialog();
                Log.e(Constants.TAG,"response : "+response);
                if(response.code() == 200){

                    try {
                        String responseBody = response.body();
                        Log.e(Constants.TAG,"responseBody : "+responseBody);
                        bookingAlert.setMessage("Booking confirmed for Vehicle no. "+vehNoET.getText().toString().trim().toUpperCase()+" at "+simpleDateFormat.format(date)).show();
                        createNotification();
                    }catch (Exception e){
                        e.printStackTrace();
                    }
                }else{
                    Log.e(Constants.TAG,"Error occurred with response code : "+response.code());
                    Snackbar.make(coordinator, "Error occurred with response code : "+response.code(), Snackbar.LENGTH_SHORT).show();
                }

            }

            @Override
            public void onFailure(Call<String> call, Throwable t) {
                call.cancel();
                Utility.closeProgressDialog();
                Log.e(Constants.TAG,"Request Failed!"+t.getLocalizedMessage());
                Snackbar.make(coordinator, "Request Failed!", Snackbar.LENGTH_SHORT).show();
            }
        });

    }
}
