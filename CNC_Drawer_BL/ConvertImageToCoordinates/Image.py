from PIL import Image, ImageDraw
import os
import datetime

black_cords = []
hour = 0
minute = 0
second = 0

def convert_image_in_coords(path, zoom, filter):
    global black_cords
    input_image = Image.open(path)    # Open image.

    # Size regulation.
    if input_image.width * zoom > 1500 or input_image.height * zoom > 1500:
        print("Image too large")
        return "0"

    ok = False
    black_cords = [(0, 0)]    # Black pixels coordinates.

    for y in range(0, input_image.height):
        for x in range(input_image.width):
            # check black pixel and write start coordinate.
            if input_image.getpixel((x, y))[0] < filter and not ok:
                black_cords.append((x * zoom, y * zoom))
                ok = True
            # check white pixel and write end coordinates.
            elif input_image.getpixel((x, y))[0] > filter and ok:
                black_cords.append((x * zoom, y * zoom))
                ok = False

    # Return in start position.
    black_cords.append((0, 0))
    # Optimization.
    black_cords = image_drawing_optimization(black_cords)

    # Image draw.
    img = Image.new("RGB", (input_image.width * zoom, input_image.height * zoom), "white")
    draw = ImageDraw.Draw(img)
    for coord in range(1, len(black_cords) - 1, 2):
        draw.line((black_cords[coord][0], black_cords[coord][1], black_cords[coord+1][0], black_cords[coord+1][1]), width=1, fill="black")
    direct = os.getcwd().replace('\\', '\\\\')
    img.save(f"{direct}\\cash\\cash.PNG")

    return "200"

# Optimizes by moving at the end to the nearest pixel in the next row.
def image_drawing_optimization(black_cords):
    help_array = []
    current_row = -1
    index = -1
    for i in black_cords:
        if current_row != i[1]:
            current_row = i[1]
            index += 1

            help_array.append([])
            help_array[index].append(i)
        else:
            if index % 2 == 0:
                help_array[index].insert(0, i)
            else:
                help_array[index].append(i)

    black_cords = []
    for i in help_array:
        for j in i:
            black_cords.append(j)
    return black_cords

#region -==- Get Time -==-

def get_time(arduino_cords):
    global hour, minute, second

    # 0.1 delay between commands.
    # 0.002 time for 1 step.
    count_seconds = len(arduino_cords) * 0.1
    count_seconds = count_seconds + get_count_steps(arduino_cords) * 0.002

    hour = int(count_seconds / 3600)    # Count hour.
    minute = int(count_seconds / 60) - hour * 60    # Count minute.
    second = int(count_seconds - minute * 60 - hour * 3600)    # Count second.

    return str(hour) + ":" + str(minute) + ":" + str(second)

def get_count_steps(arduino_cords):
    count_steps = 0
    for i in arduino_cords:
        # Filter on up & down pen.
        if i != "up" and i != "dp":
            count_steps += int(i[1:])
    return count_steps

def get_end_time():
    now = datetime.datetime.now() + datetime.timedelta(hours=hour, minutes=minute, seconds=second)    # End time.
    hour_end = now.hour
    minute_end = now.minute
    second_end = now.second
    return str(hour_end) + ":" + str(minute_end) + ":" + str(second_end)

#endregion