# **********************************************************************************
# * The MIT License (MIT)                                                          *
# * Copyright (c) 2015 by Fabrice Weinberg                                         *
# *                                                                                *
# * Permission is hereby granted, free of charge, to any person obtaining a copy   *
# * of this software and associated documentation files (the "Software"), to deal  *
# * in the Software without restriction, including without limitation the rights   *
# * to use, copy, modify, merge, publish, distribute, sublicense, and/or sell      *
# * copies of the Software, and to permit persons to whom the Software is          *
# * furnished to do so, subject to the following conditions:                       *
# * The above copyright notice and this permission notice shall be included in all *
# * copies or substantial portions of the Software.                                *
# * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR     *
# * IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,       *
# * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE    *
# * AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER         *
# * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,  *
# * OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE  *
# * SOFTWARE.                                                                      *
# **********************************************************************************

# **************************
# interaction with Arduino #
# **************************

import serial
import serial.tools.list_ports
import time

arduino = []

def get_com_ports():
    com_ports = serial.tools.list_ports.comports()    # Get list of available COM-ports on PC.
    com_ports_name = []    # List of names these COM-ports.
    # create the list of the COM-port names
    for com_port in com_ports:
        com_ports_name.append(com_port.name)

    return com_ports_name

def convert_coords_in_arduino_commands(black_coords):
    global arduino

    # List of commands.
    arduino = []

    # Convert coordinates in arduino commands
    for i in range(1, len(black_coords)):
        if i % 2 == 1:
            arduino.append("up")    # Up pen.
        else:
            arduino.append("dp")    # down pen.

        dx = black_coords[i][0] - black_coords[i - 1][0]
        dy = black_coords[i][1] - black_coords[i - 1][1]
        if dx > 0:
            # Move right. r(step)
            arduino.append("r" + str(dx))

        elif dx < 0:
            # Move left. l(step)
            arduino.append("l" + str(-dx))

        if dy > 0:
            # Move down. d(step)
            arduino.append("d" + str(dy))
        elif dy < 0:
            # Move up. u(step)
            arduino.append("u" + str(-dy))

def send_commands_to_arduino(com):
    # initialization of com-port
    s = serial.Serial(port=com, baudrate=1000000, timeout=1, bytesize=8, stopbits=1)
    print("serial Init")
    time.sleep(1)    # delay for initialization.

    # Sending commands to Arduino.
    for i in arduino:
        print("start")
        time.sleep(0.1)    # delay for more correction.
        s.write(i.encode("cp1251"))    # Send command.

        while 1:
            # Wait arduino answer.
            if s.in_waiting > 0:
                answer = s.readline()     # Read answer.
                s.reset_input_buffer()    # Clear buffer.
                print(answer)
                print(answer.decode("cp1251"))
                break
