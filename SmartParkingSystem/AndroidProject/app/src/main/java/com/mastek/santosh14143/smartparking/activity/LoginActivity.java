package com.mastek.santosh14143.smartparking.activity;

import android.app.AlertDialog;
import android.app.ProgressDialog;
import android.content.Context;
import android.content.DialogInterface;
import android.content.Intent;
import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.design.widget.CoordinatorLayout;
import android.support.design.widget.Snackbar;
import android.support.v7.app.AppCompatActivity;
import android.util.Log;
import android.view.View;
import android.widget.Button;
import android.widget.EditText;
import android.widget.TextView;
import android.widget.Toast;

import com.google.gson.Gson;
import com.mastek.santosh14143.smartparking.R;
import com.mastek.santosh14143.smartparking.model.User;
import com.mastek.santosh14143.smartparking.retrofit.APIClient;
import com.mastek.santosh14143.smartparking.retrofit.APIInterface;
import com.mastek.santosh14143.smartparking.retrofit.response.LoginResponse;
import com.mastek.santosh14143.smartparking.util.Constants;
import com.mastek.santosh14143.smartparking.util.Utility;

import org.json.JSONException;
import org.json.JSONObject;

import java.util.Hashtable;
import java.util.Map;

import retrofit2.Call;
import retrofit2.Callback;
import retrofit2.Response;

/**
 * Created by santosh14143 on 4/13/2018.
 */

public class LoginActivity extends AppCompatActivity {
    TextView register, ownerRegister;
    Button loginBtn;
    EditText usernameET,passwordET;
//    ProgressDialog progressDialog;
    CoordinatorLayout rootView;
    AlertDialog.Builder alert;
    Utility utility;
    @Override
    protected void onCreate(@Nullable Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.login_layout);
        utility = Utility.getInsatnce(this.getBaseContext());
        rootView = findViewById(R.id.rootView);
        loginBtn = findViewById(R.id.login_submit_btn);
        usernameET = findViewById(R.id.login_username);
        passwordET = findViewById(R.id.login_password);
        loginBtn.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                /*startActivity(new Intent(LoginActivity.this, DashBoardActivity.class));
                finish();*/
                String username = usernameET.getText().toString().trim();
                String password = passwordET.getText().toString().trim();
                if(validateUsernamePassword(username,password)) {
//                    progressDialog.show();
                    doLogin(username,password);
                }
            }
        });

        register = findViewById(R.id.login_register);
        register.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                startActivity(new Intent(LoginActivity.this,RegisterActivity.class));
            }
        });

        ownerRegister = findViewById(R.id.login_registerOwner);
        ownerRegister.setOnClickListener(new View.OnClickListener() {
            @Override
            public void onClick(View view) {
                startActivity(new Intent(LoginActivity.this,RegisterActivity.class));
            }
        });

        /*progressDialog = new ProgressDialog(this);
        progressDialog.setTitle("Logging In");
        progressDialog.setMessage("Please Wait...");*/

        alert = new AlertDialog.Builder(this)
                .setTitle("Response")
                .setPositiveButton("Ok", new DialogInterface.OnClickListener() {
                    @Override
                    public void onClick(DialogInterface dialogInterface, int i) {
                        startActivity(new Intent(LoginActivity.this, DashBoardActivity.class));
                finish();
                    }
                });
    }

    public boolean validateUsernamePassword(String username, String password){
        if(!username.trim().equals("") && !password.trim().equals("")){
            return true;
        }else{
            Snackbar snackbar = Snackbar.make(rootView,getString(R.string.invalid_msg),Snackbar.LENGTH_SHORT);
            snackbar.show();
        }
        return false;
    }

    private void doLogin(String username, String password){
        Utility.showProgressDialog(this,"Logging In", "Please Wait...");
        APIInterface apiInterface = APIClient.getClient().create(APIInterface.class);
        Map<String, String> requestMap = new Hashtable<>();
        requestMap.put("username",username);
        requestMap.put("password",password);
        Call<String> call = apiInterface.doLogin(requestMap);
        call.enqueue(new Callback<String>() {
            @Override
            public void onResponse(Call<String> call, Response<String> response) {
                Utility.closeProgressDialog();
                int status = 3;
                if(response.code() == 200){
                    String loginResponse = response.body();
                    Log.e(Constants.TAG,"loginResponse : "+loginResponse);
                    try {
                        JSONObject responseJson = new JSONObject(loginResponse);
                         status = responseJson.getInt("Status");
                         JSONObject classObj = responseJson.getJSONObject("classobject");
                        Log.e(Constants.TAG,"classobject : "+responseJson.getJSONObject("classobject"));
//                        status = loginResponse.getStatus();
                        Snackbar snackbar ;
                         switch (status){
                             case 0:
                                 User user = new User();
                                 user.setId(classObj.getString("_id"));
                                 user.setUsername(classObj.getString("UserName"));
                                 user.setFirstName(classObj.getString("FirstName"));
                                 user.setLastName(classObj.getString("LastName"));
                                 user.setPassword(classObj.getString("Password"));
                                 user.setEmail(classObj.getString("Email"));
                                 user.setPhone(classObj.getString("Phone"));
                                 utility.addStringPreference(Constants.USER, utility.convertObjectToString(user));
                                 startActivity(new Intent(LoginActivity.this, DashBoardActivity.class));
                                 finish();
                                 break;
                             case 1:
//                                 Toast.makeText(LoginActivity.this,"Invalid username or password",Toast.LENGTH_SHORT).show();
                                  snackbar= Snackbar.make(rootView, "Invalid username or password", Snackbar.LENGTH_SHORT);
                                 snackbar.show();
                                 break;
                             case 2:
                                 snackbar = Snackbar.make(rootView, "User does not exists ", Snackbar.LENGTH_SHORT);
                                 snackbar.show();
                                 break;
                             case 3:
                                 snackbar = Snackbar.make(rootView, "Error occurred with response code : "+response.code(), Snackbar.LENGTH_SHORT);
                                 snackbar.show();
                                 break;
                         }
                    } catch (JSONException e) {
                        e.printStackTrace();
                    }catch (Exception e){
                        e.printStackTrace();
                    }
                    /*alert.setMessage("loginResponse : "+loginResponse);
                    alert.show();*/
                }else{
                    Log.e(Constants.TAG,"Error occurred with response code : "+response.code());
                    Snackbar snackbar = Snackbar.make(rootView, "Error occurred with response code : "+response.code(), Snackbar.LENGTH_SHORT);
                    snackbar.show();
                }
            }

            @Override
            public void onFailure(Call<String> call, Throwable t) {
                call.cancel();
                Utility.closeProgressDialog();
                Log.e(Constants.TAG,"Request Failed! :"+t.getLocalizedMessage());
                Snackbar snackbar = Snackbar.make(rootView, "Request Failed!", Snackbar.LENGTH_SHORT);
                snackbar.show();
            }
        });

    }

    @Override
    public void onBackPressed() {
        System.exit(-1);
    }
}
