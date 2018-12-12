package com.mastek.santosh14143.smartparking.fragments;

import android.os.Bundle;
import android.support.annotation.Nullable;
import android.support.v4.app.Fragment;
import android.view.LayoutInflater;
import android.view.View;
import android.view.ViewGroup;
import android.widget.EditText;

import com.mastek.santosh14143.smartparking.R;
import com.mastek.santosh14143.smartparking.activity.DashBoardActivity;
import com.mastek.santosh14143.smartparking.model.User;
import com.mastek.santosh14143.smartparking.util.Constants;
import com.mastek.santosh14143.smartparking.util.Utility;

/**
 * Created by santosh14143 on 4/17/2018.
 */

public class ProfileFragment extends Fragment {
    View profileView;
    EditText firstNameTI;
    EditText lastNameTI;
    EditText emailTI;
    EditText phoneTI;
    Utility utility;
    User user;

    @Nullable
    @Override
    public View onCreateView(LayoutInflater inflater, @Nullable ViewGroup container, @Nullable Bundle savedInstanceState) {
        profileView = inflater.inflate(R.layout.profile_layout,container,false);
        ((DashBoardActivity)getActivity()).getSupportActionBar().setTitle("MyProfile");

        utility = Utility.getInsatnce(this.getActivity());
        user = (User)utility.convertStringToObject(utility.getStringPreference(Constants.USER),User.class);

        firstNameTI = profileView.findViewById(R.id.profile_first_name);
        lastNameTI = profileView.findViewById(R.id.profile_last_name);
        emailTI = profileView.findViewById(R.id.profile_email);
        phoneTI = profileView.findViewById(R.id.profile_phone);

        firstNameTI.setHint(user.getFirstName());
        lastNameTI.setHint(user.getLastName());
        emailTI.setHint(user.getEmail());
        phoneTI.setHint(user.getPhone());
        return profileView;
    }
}
