
#include "libraries/ArduinoJson.h"
#include "libraries/Message.h"
#include <Adafruit_NeoPixel.h>

Adafruit_NeoPixel strip = Adafruit_NeoPixel(5, 3, NEO_RGB + NEO_KHZ800);
int ledPin = 3;
int fsrPin = 0;
int fsrReading;
bool pressed = false;
int messageCount = 0;
int mess =0;

//int toSend = 0;

void setup() 
{
  networkit_setup();      //please leave here
 
  //TODO Your code here
  // pinMode(ledPin, OUTPUT);
  
  strip.begin();
  strip.show();
  delay(100);
  //analogWrite(ledPin,254);
  strip.setPixelColor(0, 0, 255, 0);
  strip.show();
 // pinMode(buttonPin, INPUT_PULLUP);
  Serial.println("hi"); 
}



void loop() 
{
  
  fsrReading = analogRead(fsrPin);  
  networkit_loop();       //please leave here

  //TODO Your code here
  if (fsrReading >30){
   if (fsrReading < 200 ) {
      Serial.println(" - Light touch");
      mess =1;
    } else if (fsrReading < 500) {
      Serial.println(" - Light squeeze");
      mess = 2;
    } else if (fsrReading < 800) {
      Serial.println(" - Medium squeeze");
      strip.setPixelColor(0, 255,165 , 0);
      strip.show();
      mess = 3;
    } else {
      delay(100);
      strip.setPixelColor(0, 255,0 , 0);
      strip.show();
      delay(100);
      strip.setPixelColor(0, 0,0 , 0);
      strip.show();
      delay(100);
      strip.setPixelColor(0, 255,0 , 0);
      strip.show();
      delay(100);
      Serial.println(" - Big squeeze");
      mess = 4;
    }
    delay(100);
  }
    if (mess>1){
    Message* m = new Message("force");
    m->deliverToSelf = true;
    m->addField("num1", new String(13));
    m->addField("num2", new String(5));
    messageCount++;
    m->addField("count", new String(messageCount));
    m->addField("message", new String(mess));


    sendMessage(*m);
    delete m;
    mess=0;
    }
    //delay(100);
    strip.setPixelColor(0, 0,1 , 0);
    strip.show();
  //send message demo
    
    
  
}


void messageEvent(Message& message)
{
  //TODO your code here

    Serial.println("i have a message"); 
  //demo, blinks some number of times depending on the message sent
  if (message.subject->equals("progress"))
  { 
    
      int mess = 0;
      mess = message.getField("progress")->toInt();
      Serial.println("progress" + mess); 
      if (mess>0 and mess <=50)
      {
         strip.setPixelColor(0, 0,mess/255 , 0);
        
      }
      else if(mess>50 and mess <=90)
      {
         strip.setPixelColor(0, 165,100 , 0);
      }
      else {

        strip.setPixelColor(0,0, 255 , 0);
      }
       strip.show();
       delay(500);
    
  }
}


void connectEvent()
{
  //TODO your code here
}

void disconnectEvent()
{
  //TODO your code here
}

void errorEvent(JsonObject& message)
{
  //TODO your code here
}




//=====================================================================================
//networkit library
//Arduino Uno 2K RAM
//Arduino Mega 2560 8K Ram


String inString;


void networkit_setup()
{
  Serial.begin(115200);       //Uses Baud rate of 115200, set in serial monitor for debugging
}

void networkit_loop()
{
  while (Serial.available() > 0) 
  {
    int inChar = Serial.read();

    // if you get a newline, print the string,
    // then the string's value:
    if (inChar == '\n') {
      Serial.println("Recv=" + inString);

      parseCommand(inString);
       // clear the string for new input:
      inString = "";
    }

    inString += (char)inChar;
  }
}



//Sending
const char* NETWORK_SEND_HEADER = "send_networkit_message=";
const char* NETWORK_SEND_END_HEADER = "=send_end";
//if your message isnt being written properly, increase this buffer, see calculator: https://bblanchon.github.io/ArduinoJson/assistant/
StaticJsonBuffer<600> jsonWriteBuffer;
void sendMessage(Message& m)
{
  //create message object
  JsonObject& messJson = jsonWriteBuffer.createObject();
  messJson["subject"] = *m.subject;
  messJson["deliverToSelf"] = m.deliverToSelf;
  
  JsonArray& fieldsJson = messJson.createNestedArray("fields");
  for (int i = 0; i < m.getFieldCount() ;i++)
  {
    //add each field object
    JsonObject& f = jsonWriteBuffer.createObject();
    f["key"] = *m.getKey(i);
    f["value"] = *m.getValue(i);
    fieldsJson.add(f);
  }

  //send message to serial to send on socket.io
 Serial.print(NETWORK_SEND_HEADER);
  messJson.printTo(Serial);
  Serial.println(NETWORK_SEND_END_HEADER);
  
  jsonWriteBuffer.clear();
}


//Reading
//if your message isnt being read properly, increase this buffer, see calculator: https://bblanchon.github.io/ArduinoJson/assistant/
StaticJsonBuffer<600> jsonReadBuffer;
void parseCommand(String cmd)
{
  JsonObject& eventJson = jsonReadBuffer.parseObject(cmd);

  const char* eventName = eventJson["event_name"];
  JsonObject& args = eventJson["args"];
  emitEvent(eventName, args);

  jsonReadBuffer.clear();     //clear buffer so we're ready for the next command
}


void emitEvent(const char* eventName, JsonObject& args)
{
  if (strcmp(eventName, "message") == 0)
  {
    //message got

    //convert to message object
    String subject = args["subject"];
    Message* m = new Message(subject);

    JsonArray& fields = args["fields"];
    const char* key;
    const char* value;
    for (int i =0 ; i < fields.size() ; i++)
    {

      JsonObject& fieldObj = fields[i];
      key = fieldObj["key"];
      value = fieldObj["value"];
      m->addField(key, value);
    }
    
    messageEvent(*m);

    delete m;
  }
  else if (strcmp(eventName, "connect") == 0)
  {
    connectEvent();
  }
  else if (strcmp(eventName, "disconnect") == 0)
  {
    disconnectEvent();
  }
  else if (strcmp(eventName, "error") == 0)
  {
    errorEvent(args);
  }
}

