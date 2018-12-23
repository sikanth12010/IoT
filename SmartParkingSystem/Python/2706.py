#https://raspi.tv/2014/rpi-gpio-update-and-detecting-both-rising-and-falling-edges

from gpiozero import LED
from signal import pause
import RPi.GPIO as GPIO
import time
import requests
from time import sleep     # this lets us have a time delay (see line 12)

import pymongo
from pymongo import MongoClient

GPIO.setmode(GPIO.BCM)

LED_PIN = 04
IR_PIN17 = 17
IR_PIN27 = 27

pin17_status = 0
pin27_status = 0

indicator = LED(LED_PIN)
GPIO.setup(IR_PIN17, GPIO.IN)
GPIO.setup(IR_PIN27, GPIO.IN)

client = MongoClient("mongodb+srv://admin:admin@cluster0-hfxr5.mongodb.net/carparkdb")

db = client.carparkdb

print("Ready")

# Define a threaded callback function to run in another thread when events are detected  
def car_status17(channel):
    global pin17_status
    if GPIO.input(IR_PIN17):     # if port 25 == 1        
        #print(pin17_status)
        if pin17_status == 1:
            url = "http://parkingservice20181213.azurewebsites.net/api/SlotEmpty/5af30d69cdafc0b5115d574f/17"
            data = ""
            headers = {'content-type':'application/json'}
            r = requests.put(url, data, headers=headers)        
            print("Car Slot 17 Empty API status {:>3} ".format(r.status_code))
            #print("slot 17 empty")
            pin17_status = 0
        
    else:                  # if port 25 != 1        
        #print(pin17_status)
        if pin17_status == 0:
            url = "http://parkingservice20181213.azurewebsites.net/api/SlotBook/Booked?carpark_id=5af30d69cdafc0b5115d574f&slot_id=17" # Call API to update slot status to Confirmed
            data = ""
            headers = {'content-type':'application/json'}
            #r = requests.get(url, data, headers=headers)
            #print("Car Slot 17 Occupied API status {:>3} ".format(r.status_code))
            print("slot 17 occupied")
            pin17_status = 1
 
def car_status27(channel):    
    global pin27_status
    if GPIO.input(IR_PIN27):     # if port 25 == 1
        if pin27_status == 1:
            url = "http://parkingservice20181213.azurewebsites.net/api/SlotEmpty/5af30d69cdafc0b5115d574f/27"
            data = ""
            headers = {'content-type':'application/json'}
            r = requests.put(url, data, headers=headers)        
            #print("Car Slot 27 Empty API status {:>3} ".format(r.status_code))
            print("slot 27 empty")
            pin27_status = 0
        
    else:                  # if port 25 != 1
        if pin27_status==0:
            url = "http://parkingservice20181213.azurewebsites.net/api/SlotBook/Booked?carpark_id=5af30d69cdafc0b5115d574f&slot_id=27" # Call API to update slot status to Confirmed
            data = ""
            headers = {'content-type':'application/json'}
            r = requests.get(url, data, headers=headers)
            #print("Car Slot 27 Occupied API status {:>3} ".format(r.status_code))
            print("slot 27 occupied")        
            pin27_status = 1
 
  
GPIO.add_event_detect(IR_PIN17, GPIO.BOTH, callback=car_status17, bouncetime=5000)
GPIO.add_event_detect(IR_PIN27, GPIO.BOTH, callback=car_status27, bouncetime=5000)


try:  
    sleep(3000)         # wait 3000 seconds  
    print "Time's up. Finished!"

finally:                   # this block will run no matter how the try block exits  
    GPIO.cleanup()         # clean up after yourself  