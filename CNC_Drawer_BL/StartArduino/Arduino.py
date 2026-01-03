import serial
import serial.tools.list_ports
import time

arduino = []

def get_com_ports():
    com_ports = serial.tools.list_ports.comports()  # Get list of available COM-ports on PC.
    com_ports_name = []  # List of names these COM-ports.
    # Goes through the list and add in "com_ports_name" the name of these COM-ports
    for com_port in com_ports:
        com_ports_name.append(com_port.name)

    return com_ports_name

def convert_coords_in_arduino_commands(black_cords):
    global arduino

    # List with commands.
    arduino = []

    # Convert coordinates in arduino commands
    for i in range(1, len(black_cords)):
        if i % 2 == 1:
            arduino.append("up")    # Up pen.
        else:
            arduino.append("dp")    # down pen.

        if black_cords[i][0] - black_cords[i - 1][0] > 0:
            arduino.append("r" + str(black_cords[i][0] - black_cords[i - 1][0]))    # Move right. r(step)
        elif black_cords[i][0] - black_cords[i - 1][0] < 0:
            arduino.append("l" + str(black_cords[i - 1][0] - black_cords[i][0]))    # Move left. l(step)

        if black_cords[i][1] - black_cords[i - 1][1] > 0:
            arduino.append("d" + str(black_cords[i][1] - black_cords[i - 1][1]))    # Move down. d(step)
        elif black_cords[i][1] - black_cords[i - 1][1] < 0:
            arduino.append("u" + str(black_cords[i - 1][1] - black_cords[i][1]))    # Move up. u(step)

def send_commands_to_arduino(com):
    # init com-port
    s = serial.Serial(port="COM4", baudrate=1000000, timeout=1, bytesize=8, stopbits=1)
    print("serial Init")
    time.sleep(1)    # delay for initialization.

    # Sending commands on Arduino.
    for i in arduino:
        print("start")
        time.sleep(0.1)    # delay for more correction.
        s.write(i.encode("cp1251"))    # Send command.

        while 1:
            # Wait arduino answer.
            if s.in_waiting > 0:
                answer = s.readline()    # Read answer.
                s.reset_input_buffer()    # Clear buffer.
                print(answer)
                print(answer.decode("cp1251"))
                break
