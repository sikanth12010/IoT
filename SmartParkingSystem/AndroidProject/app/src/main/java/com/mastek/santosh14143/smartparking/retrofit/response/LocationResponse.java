package com.mastek.santosh14143.smartparking.retrofit.response;

import android.os.Parcel;
import android.os.Parcelable;

import java.util.List;

public class LocationResponse  implements Parcelable{
    String name;
    int tspaces;
    int aspaces;
    Loc loc;
    String _id;

    protected LocationResponse(Parcel in) {
        name = in.readString();
        tspaces = in.readInt();
        aspaces = in.readInt();
        _id = in.readString();
    }

    public static final Creator<LocationResponse> CREATOR = new Creator<LocationResponse>() {
        @Override
        public LocationResponse createFromParcel(Parcel in) {
            return new LocationResponse(in);
        }

        @Override
        public LocationResponse[] newArray(int size) {
            return new LocationResponse[size];
        }
    };

    @Override
    public int describeContents() {
        return 0;
    }

    @Override
    public void writeToParcel(Parcel parcel, int i) {
        parcel.writeString(name);
        parcel.writeInt(tspaces);
        parcel.writeInt(aspaces);
        parcel.writeString(_id);
    }

    public class Loc{
        String type ;
        List<Double> coordinates;

        public String getType() {
            return type;
        }

        public List<Double> getCoordinates() {
            return coordinates;
        }
    }

    public String getName() {
        return name;
    }

    public int getTspaces() {
        return tspaces;
    }

    public int getAspaces() {
        return aspaces;
    }

    public void setAspaces(int aspaces) {
        this.aspaces = aspaces;
    }

    public Loc getLoc() {
        return loc;
    }

    public String get_id() {
        return _id;
    }

    @Override
    public String toString() {
        return "LocationResponse{" +
                "name='" + name + '\'' +
                ", tspaces='" + tspaces + '\'' +
                ", aspaces='" + aspaces + '\'' +
                ", loc=" + loc +
                ", _id='" + _id + '\'' +
                '}';
    }
}
