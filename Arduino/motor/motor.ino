/**
 **********************************************************************************
 * The MIT License (MIT)                                                          *
 * Copyright (c) 2015 by Fabrice Weinberg                                         *
 *                                                                                *
 * Permission is hereby granted, free of charge, to any person obtaining a copy   *
 * of this software and associated documentation files (the "Software"), to deal  *
 * in the Software without restriction, including without limitation the rights   *
 * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell      *
 * copies of the Software, and to permit persons to whom the Software is          *
 * furnished to do so, subject to the following conditions:                       *
 * The above copyright notice and this permission notice shall be included in all *
 * copies or substantial portions of the Software.                                *
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR     *
 * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,       *
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE    *
 * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER         *
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,  *
 * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE  *
 * SOFTWARE.                                                                      *
 **********************************************************************************
 */

/** 2D plotter motor control driver
*/

#include <Servo.h>    // Import a library for servo control.
Servo myServo;        // Initialization servo contol.

// pins of the first engine:
#define step_1 A0    // step pulses.
#define dir_1 A1     // direction of rotation.
#define enable_1 38  // inclusion.

// pins of the second engine:
#define step_2 A6    // step pulses.
#define dir_2 A7     // direction of rotation.
#define enable_2 A2  // inclusion.

// pins of the servo:
#define servo_control_pin 11     // servo digital pin number.
#define servo_up_command 15          // servo degrees for raised pencil.
#define servo_down_command 30        // servo degrees for lowered pencil.

int speed = 2000;

void setup() {
  myServo.attach(servo_control_pin);    // servo initialization.
  myServo.write(servo_up_command);

  // initialization of the first engine pins.
  pinMode(step_1, OUTPUT);
  pinMode(dir_1, OUTPUT);
  pinMode(enable_1, OUTPUT);

  // initialization of the second engine pins.
  pinMode(step_2, OUTPUT);
  pinMode(dir_2, OUTPUT);
  pinMode(enable_2, OUTPUT);

  digitalWrite(enable_1, HIGH);  // turn off.
  digitalWrite(enable_2, HIGH);  // turn off.
  
  Serial.begin(1000000); // Set COM-port speed 1 million bouds. (0,5 million is enough, but to be on the safe side it’s better to do it this way)
  Serial.setTimeout(1);
}

// move the pencil forward by "len" steps.
void up(int len)
{
  digitalWrite(dir_1, LOW);    // Set direction.
  for(int i = 0; i < len; i++)
  {
    digitalWrite(enable_1, LOW);     // Turn on.
    delayMicroseconds(speed);        // Delay between step.
    digitalWrite(step_1, HIGH);      // Step ON.
    digitalWrite(step_1, LOW);       // Step OFF.
    digitalWrite(enable_1, HIGH);    // Turn off.
  }
}

// move the pencil back by "len" steps.
void down(int len)
{
  digitalWrite(dir_1, HIGH);    // Set direction.
  for(int i = 0; i < len; i++)
  {
    digitalWrite(enable_1, LOW);     // Turn on.
    delayMicroseconds(speed);        // Delay between step.
    digitalWrite(step_1, HIGH);      // Step ON.
    digitalWrite(step_1, LOW);       // Step OFF.
    digitalWrite(enable_1, HIGH);    // Turn off.
  }
}

// move the pencil to the right by "len" steps.
void right(int len)
{
  digitalWrite(dir_2, LOW);    // Set direction.
  for(int i = 0; i < len; i++)
  {
    digitalWrite(enable_2, LOW);     // Turn on.
    delayMicroseconds(speed);        // Delay between step.
    digitalWrite(step_2, HIGH);      // Step ON.
    digitalWrite(step_2, LOW);       // Step OFF.
    digitalWrite(enable_2, HIGH);    // Turn off.
  }
}

// move the pencil to the left by "len" steps.
void left(int len)
{
  digitalWrite(dir_2, HIGH);    // Set direction.
  for(int i = 0; i < len; i++)
  {
    digitalWrite(enable_2, LOW);     // Turn on.
    delayMicroseconds(speed);        // Delay between step.
    digitalWrite(step_2, HIGH);      // Step ON.
    digitalWrite(step_2, LOW);       // Step OFF.
    digitalWrite(enable_2, HIGH);    // Turn off.
  }
}

void loop() {
  if(Serial.available() > 0)
  {
    String a = Serial.readString();    // Read command from COM-port.
    // decoding command
    if(a.substring(0, 2) == "up")
    {
      myServo.write(servo_up_command);
    }
    else if(a.substring(0, 2) == "dp")
    {
      myServo.write(servo_down_command);
    }
    else if(a[0] == 'r')
    {
      int len = a.substring(1).toInt();
      right(len);
    }
    else if(a[0] == 'l')
    {
      int len = a.substring(1).toInt();
      left(len);
    }
    else if(a[0] == 'u')
    {
      int len = a.substring(1).toInt();
      up(len);
    }
    else if(a[0] == 'd')
    {
      int len = a.substring(1).toInt();
      down(len);
    }

    Serial.println(a);    // sending a message about the executed command.
  }
}
