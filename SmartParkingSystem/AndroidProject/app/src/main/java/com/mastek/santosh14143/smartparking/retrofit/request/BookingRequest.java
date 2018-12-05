package com.mastek.santosh14143.smartparking.retrofit.request;

import com.google.gson.annotations.SerializedName;

public class BookingRequest {
    @SerializedName("car_park_id")
    String car_park_id;
    @SerializedName("cust_id")
    String cust_id;
    @SerializedName("vehicle_no")
    String vehicle_no;

    public void setCar_park_id(String car_park_id) {
        this.car_park_id = car_park_id;
    }

    public void setCust_id(String cust_id) {
        this.cust_id = cust_id;
    }

    public void setVehicle_no(String vehicle_no) {
        this.vehicle_no = vehicle_no;
    }
}
