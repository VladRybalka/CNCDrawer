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

/** Скетч керування двигунами 2D плоттера 
*/

#include <Servo.h>
Servo myServo;

// № пінів двигуна 1:
#define step_1 A0    // крокові імпульси
#define dir_1 A1     // напрямок обертання
#define enable_1 38  // вмикання

// № пінів двигуна 1:
#define step_2 A6    // крокові імпульси
#define dir_2 A7     // напрямок обертання
#define enable_2 A2  // вмикання

// для сервоприводу:
#define servo_control 11     // № піну керування
#define servo_up 15  // команда "підняти олівець"
#define servo_down 30  // команда "опустити олівець"

int speed = 2000;    // швидкість руху

void setup() {
  // значення для сервоприводу: 30 - опустити, 15 - підняти.
  myServo.attach(servo_control);  // № піна для сервоприводу
  myServo.write(servo_up);

  // ініціалізація пінів двигуна 1.
  pinMode(step_1, OUTPUT);
  pinMode(dir_1, OUTPUT);
  pinMode(enable_1, OUTPUT);

  // ініціалізація пінів двигуна 2.
  pinMode(step_2, OUTPUT);
  pinMode(dir_2, OUTPUT);
  pinMode(enable_2, OUTPUT);

  digitalWrite(enable_1, HIGH);  // вимкнути
  digitalWrite(enable_2, HIGH);  // вимкнути
  
  Serial.begin(1000000); // встановлення швидкості COM порту 9600 бод.
  Serial.setTimeout(1);
}

// рухати олівець на "len" кроків вгору
void up(int len)
{
  digitalWrite(dir_1, LOW);
  for(int i = 0; i < len; i++)
  {
    digitalWrite(enable_1, LOW);
    delayMicroseconds(speed);
    digitalWrite(step_1, HIGH);
    //delayMicroseconds(speed);
    digitalWrite(step_1, LOW);
    //delayMicroseconds(speed);
    digitalWrite(enable_1, HIGH);
  }
}

// рухати олівець на "len" кроків вниз.
void down(int len)
{
  digitalWrite(dir_1, HIGH);
  for(int i = 0; i < len; i++)
  {
    digitalWrite(enable_1, LOW);
    delayMicroseconds(speed);
    digitalWrite(step_1, HIGH);
    //delayMicroseconds(speed);
    digitalWrite(step_1, LOW);
    //delayMicroseconds(speed);
    digitalWrite(enable_1, HIGH);
  }
}

// рухати олівець на "len" кроків праворуч.
void right(int len)
{
  digitalWrite(dir_2, LOW);
  for(int i = 0; i < len; i++)
  {
    digitalWrite(enable_2, LOW);
    delayMicroseconds(speed);
    digitalWrite(step_2, HIGH);
    //delayMicroseconds(speed);
    digitalWrite(step_2, LOW);
    //delayMicroseconds(speed);
    digitalWrite(enable_2, HIGH);
  }
}

// рухати олівець на "len" кроків ліворуч.
void left(int len)
{
  digitalWrite(dir_2, HIGH);
  for(int i = 0; i < len; i++)
  {
    digitalWrite(enable_2, LOW);
    delayMicroseconds(speed);
    digitalWrite(step_2, HIGH);
    //delayMicroseconds(speed);
    digitalWrite(step_2, LOW);
    //delayMicroseconds(speed);
    digitalWrite(enable_2, HIGH);
  }
}

void loop() {
  if(Serial.available() > 0)
  {
    String a = Serial.readString();    // зчитування команди з COM-порту
    // декодування команди
    if(a.substring(0, 2) == "up")
    {
      myServo.write(servo_up);
    }
    else if(a.substring(0, 2) == "dp")
    {
      myServo.write(servo_down);
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

    Serial.println(a);    // відправка повідомлення про виконану команду
  }
}
