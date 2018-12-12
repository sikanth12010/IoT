package com.mastek.santosh14143.smartparking.util;

import android.app.ProgressDialog;
import android.content.Context;
import android.content.SharedPreferences;

import com.google.gson.Gson;
import com.mastek.santosh14143.smartparking.model.User;
import com.mastek.santosh14143.smartparking.retrofit.APIClient;
import com.mastek.santosh14143.smartparking.retrofit.APIInterface;

import retrofit2.Call;

public class Utility {
    private static Utility utility;
    private Context context;
    private SharedPreferences sharedPreferences;
    private SharedPreferences.Editor editor;
    private static ProgressDialog progressDialog;

    private Utility(Context context){
        this.context = context;
        sharedPreferences = context.getSharedPreferences(Constants.PREFERENCE_NAME,Context.MODE_PRIVATE);
        editor = sharedPreferences.edit();
    }

    public static Utility getInsatnce(Context context){
        if(utility==null){
            utility = new Utility(context);
        }
        return utility;
    }

    public void clearPreference(){
        editor.clear();
        editor.commit();
    }

    public void addStringPreference(String key, String value){
        editor.putString(key,value);
        editor.commit();
    }

    public String getStringPreference(String key){
        return sharedPreferences.getString(key,"");
    }

    public void addBooleanPreference(String key, boolean value){
        editor.putBoolean(key,value);
        editor.commit();
    }

    public boolean getBooleanPreference(String key){
        return sharedPreferences.getBoolean(key,false);
    }

    public static  void showProgressDialog(Context context, String title, String msg){
        if(progressDialog == null) {
            progressDialog = new ProgressDialog(context);
        }
        progressDialog.setTitle(title);
        progressDialog.setMessage(msg);
        progressDialog.show();
    }

    public  static  void closeProgressDialog(){
        if(progressDialog!=null && progressDialog.isShowing()) {
            progressDialog.dismiss();
            progressDialog = null;
        }
    }

    public String convertObjectToString(Object object){
        Gson gson = new Gson();
        return gson.toJson(object);
    }

    public Object convertStringToObject(String string, Class classRef){
        Gson gson = new Gson();
        return gson.fromJson(string, classRef);
    }

   /* public void callAPI(Class requestType, Class responseType, ApiResponse response){
        APIInterface apiInterface = APIClient.getClient().create(APIInterface.class);
        Call<responseType> call = apiInterface.doLogin(requestType);
    }

    public interface ApiResponse{
        void onResponse(Object response);
    }*/
}
