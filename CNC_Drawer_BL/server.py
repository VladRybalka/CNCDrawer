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

# ************************************
# script for communication UI and Bl *
# ************************************
from flask import Flask, send_file
import json
import os
import ConvertImageToCoordinates.Image as ConvertToCoord
import StartArduino.Arduino
from StartArduino.Arduino import convert_coords_in_arduino_commands, send_commands_to_arduino

app = Flask(__name__)

# Returns all available COM-ports.
@app.route('/com')
def com_ports():
    return json.dumps(StartArduino.Arduino.get_com_ports())

#region -==- Image -==-

# Processes the image and checks its size.
@app.route('/open_image/<string:path>/<int:zoom>/<int:filter>')
def convert(path, zoom, filter):
    path = path.replace('+', ' ')
    a = ConvertToCoord.convert_image_in_coords(path, zoom, filter)
    if a == "0":
        return "0"
    convert_coords_in_arduino_commands(ConvertToCoord.black_coords)

    direct = os.getcwd().replace('\\', '\\\\')
    return send_file(f"{direct}\\cash\\cash.PNG", mimetype='image/png')

# Returns the time it took to draw the image.
@app.route('/time')
def time():
    return ConvertToCoord.get_time(StartArduino.Arduino.arduino)

# Return the end time.
@app.route('/end_time')
def end_time():
    return ConvertToCoord.get_end_time()

# Return size(x, y)
@app.route('/get_size')
def get_size():
    return json.dumps(ConvertToCoord.get_size())

#endregion

# Start arduino command sending.
@app.route('/start/<string:com>')
def start(com):
    print(com)
    convert_coords_in_arduino_commands(ConvertToCoord.black_coords)
    send_commands_to_arduino(com)
    return "200"

# Start server on 5000 port.
app.run(port=5000)
