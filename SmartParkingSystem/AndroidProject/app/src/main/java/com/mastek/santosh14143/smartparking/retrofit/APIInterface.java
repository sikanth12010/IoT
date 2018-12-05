package com.mastek.santosh14143.smartparking.retrofit;

import com.mastek.santosh14143.smartparking.retrofit.request.BookingRequest;
import com.mastek.santosh14143.smartparking.retrofit.response.LocationResponse;
import com.mastek.santosh14143.smartparking.retrofit.response.LoginResponse;

import java.util.List;
import java.util.Map;

import retrofit2.Call;
import retrofit2.http.Body;
import retrofit2.http.GET;
import retrofit2.http.Headers;
import retrofit2.http.POST;
import retrofit2.http.QueryMap;

/**
 * Created by santosh14143 on 4/11/2018.
 */

public interface APIInterface {

    @Headers("Content-Type: application/json")
//    @POST("ValidateUser")
    @GET("user")
    Call<String> doLogin(@QueryMap Map<String,String> params);

    @POST("User")
    Call<String> doRegister(@QueryMap Map<String,String> params);

    @GET("Location/GetByLatLong")
    Call<List<LocationResponse>> getParkings(@QueryMap Map<String,String> params);

    @Headers("Content-Type: application/json")
    @POST("SlotBook/Post")
    Call<String> doBooking(@Body BookingRequest request);

}
