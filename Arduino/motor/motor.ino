#include <Servo.h>
Servo myServo;

#define step_1 A0 // крок мотор 1.
#define dir_1 A1 // напрям руху мотор 1.
#define enable_1 38 // включення мотор 1.

#define step_2 A6 // крок мотор 2.
#define dir_2 A7 // напрям руху мотор 2.
#define enable_2 A2 // включення мотор 2.

int sped = 2000; // швидкість руху.

void setup() {
  // значення серво: 30 - опустити, 15 - підняти.
  myServo.attach(11); // серво на 11 піні.
  myServo.write(10);

  // ініціалізація пінів 1 мотору.
  pinMode(step_1, OUTPUT);
  pinMode(dir_1, OUTPUT);
  pinMode(enable_1, OUTPUT);

  // ініціалізація пінів 2 мотору.
  pinMode(step_2, OUTPUT);
  pinMode(dir_2, OUTPUT);
  pinMode(enable_2, OUTPUT);

  digitalWrite(enable_1, HIGH);
  digitalWrite(enable_2, HIGH);
  
  Serial.begin(1000000); // встановлення швидкості ком порту 9600 бод.
  Serial.setTimeout(1); // ???
}

// вгору.
void up(int len)
{
  digitalWrite(dir_1, LOW);
  for(int i = 0; i < len; i++)
  {
    digitalWrite(enable_1, LOW);
    delayMicroseconds(sped);
    digitalWrite(step_1, HIGH);
    //delayMicroseconds(sped);
    digitalWrite(step_1, LOW);
    //delayMicroseconds(sped);
    digitalWrite(enable_1, HIGH);
  }
}

// вниз.
void down(int len)
{
  digitalWrite(dir_1, HIGH);
  for(int i = 0; i < len; i++)
  {
    digitalWrite(enable_1, LOW);
    delayMicroseconds(sped);
    digitalWrite(step_1, HIGH);
    //delayMicroseconds(sped);
    digitalWrite(step_1, LOW);
    //delayMicroseconds(sped);
    digitalWrite(enable_1, HIGH);
  }
}

// вправо.
void right(int len)
{
  digitalWrite(dir_2, LOW);
  for(int i = 0; i < len; i++)
  {
    digitalWrite(enable_2, LOW);
    delayMicroseconds(sped);
    digitalWrite(step_2, HIGH);
    //delayMicroseconds(sped);
    digitalWrite(step_2, LOW);
    //delayMicroseconds(sped);
    digitalWrite(enable_2, HIGH);
  }
}

// вліво.
void left(int len)
{
  digitalWrite(dir_2, HIGH);
  for(int i = 0; i < len; i++)
  {
    digitalWrite(enable_2, LOW);
    delayMicroseconds(sped);
    digitalWrite(step_2, HIGH);
    //delayMicroseconds(sped);
    digitalWrite(step_2, LOW);
    //delayMicroseconds(sped);
    digitalWrite(enable_2, HIGH);
  }
}

void loop() {
  if(Serial.available() > 0)
  {
    String a = Serial.readString();
    if(a.substring(0, 2) == "up")
    {
      myServo.write(10);
    }
    else if(a.substring(0, 2) == "dp")
    {
      myServo.write(30);
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

    Serial.println(a);
  }
}
