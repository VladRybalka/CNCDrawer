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
    convert_coords_in_arduino_commands(ConvertToCoord.black_cords)

    direct = os.getcwd().replace('\\', '\\\\')
    return send_file(f"{direct}\\cash\\cash.PNG", mimetype='image/png')

# Returns the time it took to draw the image.
@app.route('/time')
def time():
    return str(ConvertToCoord.get_time(StartArduino.Arduino.arduino))

#endregion

# Start arduino command sending.
@app.route('/start/<string:com>')
def start(com):
    print(com)
    convert_coords_in_arduino_commands(ConvertToCoord.black_cords)
    send_commands_to_arduino(com)
    return "200"

# Start server on 5000 port.
app.run(port=5000)