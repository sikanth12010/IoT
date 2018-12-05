package com.mastek.santosh14143.smartparking.activity;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.design.widget.CoordinatorLayout;
import android.support.design.widget.Snackbar;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;

import com.mastek.santosh14143.smartparking.R;
import com.mastek.santosh14143.smartparking.retrofit.APIClient;
import com.mastek.santosh14143.smartparking.retrofit.APIInterface;
import com.mastek.santosh14143.smartparking.util.Constants;
import com.mastek.santosh14143.smartparking.util.Utility;

import java.util.Hashtable;
import java.util.Map;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

/**
 * Created by santosh14143 on 4/13/2018.
 */

public class RegisterActivity extends AppCompatActivity{

    CoordinatorLayout coordinatorLayout;
    EditText firstNameET;
    EditText lastNameET;
    EditText emailET;
    EditText mobileET;
    EditText passwordET;
    EditText confirmPasswordET;
    Button registerBtn;

    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.register_layout);

        coordinatorLayout = findViewById(R.id.register_coordinatorView);
        firstNameET = findViewById(R.id.register_first_name);
        lastNameET = findViewById(R.id.register_last_name);
        emailET = findViewById(R.id.register_email);
        mobileET = findViewById(R.id.register_mobile);
        passwordET = findViewById(R.id.register_password);
        registerBtn = findViewById(R.id.register_btn);
        confirmPasswordET = findViewById(R.id.register_confirm_password);

        registerBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {

                if (confirmPasswordET.getText().toString().trim().equals(passwordET.getText().toString().trim())) {
                    doRegister();
                }else{
                    Snackbar snackbar = Snackbar.make(coordinatorLayout, "Check Password!", Snackbar.LENGTH_SHORT);
                    snackbar.show();
                }
            }
        });
    }

    private void doRegister(){
        Utility.showProgressDialog(this,"Loading...", "Please wait");
        APIInterface apiInterface = APIClient.getClient().create(APIInterface.class);
        Map<String, String> requestMap = new Hashtable<>();
        requestMap.put("username",firstNameET.getText().toString().trim());
        requestMap.put("first_name",firstNameET.getText().toString().trim());
        requestMap.put("last_name",lastNameET.getText().toString().trim());
        requestMap.put("pswd_hash",passwordET.getText().toString().trim());
        requestMap.put("pswd_salt","");
        requestMap.put("email",emailET.getText().toString().trim());
        requestMap.put("phone",mobileET.getText().toString().trim());
        requestMap.put("photo","");

        Call<String> call = apiInterface.doRegister(requestMap);
        call.enqueue(new Callback<String>() {
            @Override
            public void onResponse(Call<String> call, Response<String> response) {
                Utility.closeProgressDialog();
                Log.e(Constants.TAG,"response : "+response);
                if(response.code() == 200){

                    try {
                        String responseBody = response.body();
                        Log.e(Constants.TAG,"responseBody : "+responseBody);

                    }catch (Exception e){
                        e.printStackTrace();
                    }

                }else{
                    Log.e(Constants.TAG,"Error occurred with response code : "+response.code());
                    /*Snackbar snackbar = Snackbar.make(rootView, "Error occurred with response code : "+response.code(), Snackbar.LENGTH_SHORT);
                    snackbar.show();*/
                }

            }

            @Override
            public void onFailure(Call<String> call, Throwable t) {
                call.cancel();
                Utility.closeProgressDialog();
                Log.e(Constants.TAG,"Request Failed!"+t.getLocalizedMessage());
               /* Snackbar snackbar = Snackbar.make(rootView, "Request Failed!", Snackbar.LENGTH_SHORT);
                snackbar.show();*/
            }
        });

    }
}
