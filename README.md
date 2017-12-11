# About
This project uses a combination of NetworkIt, Arduino and RFID (WPF) solutions.

## NetworkIt
A static IP, socket.io based proxy server and client to allow different platforms to communciate to each other. Designed for CPSC 581 students at the University of Calgary, the kit allows students to quickly combine different plaforms and devices together for prototyping.

This version was developed by: Kevin Ta

Originally developed by: David Ledo & Brennan Jones

The predecessor to the Original NetworkIt was [iNetwork](http://grouplab.cpsc.ucalgary.ca/cookbook/index.php/Toolkits/INetwork) by Sebastian Boring
For more information about NetworkIt, please see https://github.com/kevinta893/NetworkIt

## Arduino

Code inspiration comes from http://www.instructables.com/id/Demystifying-4-pin-addressable-RGB-LEDS/
and 
http://www.instructables.com/id/Demystifying-4-pin-addressable-RGB-LEDS/

## RFID
Base code is taken from:
https://www.phidgets.com/downloads/phidget22/examples/dotnet/csharp/RFID/Phidget22_RFID_CSharp_Windows_Ex.zip

# How to Run Everything

1. Open up RFID Visual Studio solution and run it.
2. Open up Arduino IDE and open the network_force solution. Select the Upload button.
3. Open up the command line terminal and go to the directory where arduino_network.js is located. Run command "node arduino_network.js"

