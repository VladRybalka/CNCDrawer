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


# ******************************************************* 
# Script for preparing images for transfer to a plotter #
# *******************************************************

from PIL import Image, ImageDraw
import os
import datetime

black_coords = []
hour = 0
minute = 0
second = 0

def convert_image_in_coords(path, zoom, filter):
    global black_coords
    input_image = Image.open(path)    # Open image.

    # Size validation.
    if input_image.width * zoom > 1500 or input_image.height * zoom > 1500:
        print("Image too large")
        return "0"

    ok = False
    black_coords = [(0, 0)]    # Black pixels coordinates.

    for y in range(0, input_image.height):
        for x in range(input_image.width):
            # if black pixel --> beginning coordinate of the segment.
            if input_image.getpixel((x, y))[0] < filter and not ok:
                black_coords.append((x * zoom, y * zoom))
                ok = True
            # if white pixel --> final coordinate of the segment.
            elif input_image.getpixel((x, y))[0] > filter and ok:
                black_coords.append((x * zoom, y * zoom))
                ok = False

    # Return to start position.
    black_coords.append((0, 0))
    # Optimization.
    black_coords = image_drawing_optimization(black_coords)

    # Image draw.
    img = Image.new("RGB", (input_image.width * zoom, input_image.height * zoom), "white")
    draw = ImageDraw.Draw(img)
    for coord in range(1, len(black_coords) - 1, 2):
        draw.line((black_coords[coord][0],
                   black_coords[coord][1],
                   black_coords[coord+1][0],
                   black_coords[coord+1][1]), width=1, fill="black")
    direct = os.getcwd().replace('\\', '\\\\')
    img.save(f"{direct}\\cash\\cash.PNG")

    return "200"

# Optimize unnecessary movements by jumping to the nearest pixel in the next row.
def image_drawing_optimization(black_coords):
    help_array = []
    current_row = -1
    index = -1
    for i in black_coords:
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

    black_coords = []
    for i in help_array:
        for j in i:
            black_coords.append(j)
    return black_coords

#region -==- Get Time -==-

def get_time(arduino_coords):
    global hour, minute, second

    # 0.1 delay between commands.
    # 0.002 duration of one step.
    count_seconds = len(arduino_coords) * 0.1
    count_seconds = count_seconds + get_count_steps(arduino_coords) * 0.002

    hour = int(count_seconds / 3600)                           # Count hour.
    minute = int(count_seconds / 60) - hour * 60               # Count minute.
    second = int(count_seconds - minute * 60 - hour * 3600)    # Count second.

    return f"{hour}:{minute}:{second}"

def get_count_steps(arduino_coords):
    count_steps = 0
    for i in arduino_coords:
        # Filter on up & down pen.
        if i != "up" and i != "dp":
            count_steps += int(i[1:])
    return count_steps

def get_end_time():
    now = datetime.datetime.now() + datetime.timedelta(hours=hour,
                                                       minutes=minute,
                                                       seconds=second)    # End time.
    hour_end = now.hour
    minute_end = now.minute
    second_end = now.second
    return f"{hour_end}:{minute_end}:{second_end}"

#endregion