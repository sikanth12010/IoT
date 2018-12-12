package com.mastek.santosh14143.smartparking.retrofit.response;

public class LoginResponse {
    int Status;
    ClassObject classobjectObj;

    public int getStatus() {
        return Status;
    }

    public ClassObject getClassobject() {
        return classobjectObj;
    }

    public class ClassObject{
        String UserName;
        String FirstName;
        String LastName;
        String PasswordHash;
        String PasswordSalt;
        String Email;
        Double Phone;
        String Photo;
        String Password;
        String _id;

        public String getUserName() {
            return UserName;
        }

        public String getFirstName() {
            return FirstName;
        }

        public String getLastName() {
            return LastName;
        }

        public String getPasswordHash() {
            return PasswordHash;
        }

        public String getPasswordSalt() {
            return PasswordSalt;
        }

        public String getEmail() {
            return Email;
        }

        public Double getPhone() {
            return Phone;
        }

        public String getPhoto() {
            return Photo;
        }

        public String getPassword() {
            return Password;
        }
    }
}
