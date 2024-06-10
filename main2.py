#Import stuff
from zipfile import ZipFile
import os
import shutil
import ctypes

#try:
# is_admin = os.getuid() == 0
#except AttributeError:
# is_admin = ctypes.windll.shell32.IsUserAnAdmin() != 0
#
#if is_admin == False:
#    print("⚠️ This program needs to be run as admin. ⚠️")
#    print("It needs this to add files to the Ansel folder.")
#    input("Press enter to close this.")
#    exit(1)

#Make folder to extract to
newpath = r"RTGI"
os.makedirs(newpath)
print("Made folder to extract to")

#Extract it
with ZipFile('ReShade_RTGI_0.36.1.zip', 'r') as zip_object:
    zip_object.extractall("RTGI")
print("Extracted the zip")

#Rename and duplicate files
os.rename('RTGI\\Shaders\\qUINT_rtgi.fx', 'RTGI\\Shaders\\qUINT_rtgi_diffuse.fx')
print("Renamed file")

os.rename('RTGI\\Textures\\bluenoise.png', 'RTGI\\Textures\\rtgibluenoise.png')
print("Renamed bluenoise file")



#Reshade version check deletion:

#Delete reshade version check 1
fileNameR1 = 'RTGI\\Shaders\\qUINT_rtgi_diffuse.fx'
lineNumberR1 = 19
textR1 = ""
    
with open(fileNameR1) as file:
    lines = file.readlines()

lines[lineNumberR1 - 1] = textR1 + "\n"

with open(fileNameR1, 'w') as file:
    for line in lines:
        file.write(line)


#Delete reshade version check 2
fileNameR2 = 'RTGI\\Shaders\\qUINT_rtgi_diffuse.fx'
lineNumberR2 = 20
textR2 = ""
    
with open(fileNameR2) as file:
    lines = file.readlines()

lines[lineNumberR2 - 1] = textR2 + "\n"

with open(fileNameR2, 'w') as file:
    for line in lines:
        file.write(line)


#Delete reshade version check 3
fileNameR3 = 'RTGI\\Shaders\\qUINT_rtgi_diffuse.fx'
lineNumberR3 = 21
textR3 = ""
    
with open(fileNameR3) as file:
    lines = file.readlines()

lines[lineNumberR3 - 1] = textR3 + "\n"

with open(fileNameR3, 'w') as file:
    for line in lines:
        file.write(line)
print("Removed Reshade version check")

#Reshade version check deletion end


#Set pre-processor definitions

#File modifications 1
fileName1 = 'RTGI\\Shaders\\qUINT_rtgi_diffuse.fx'
lineNumber1 = 28
text1 = " #define INFINITE_BOUNCES       1   //[0 or 1]      If enabled, path tracer samples previous frame GI as well, causing a feedback loop to simulate secondary bounces, causing a more widespread GI."
    
with open(fileName1) as file:
    lines = file.readlines()

lines[lineNumber1 - 1] = text1 + "\n"

with open(fileName1, 'w') as file:
    for line in lines:
        file.write(line)
print("Set pre-processor definition")


#File modifications 2
fileName2 = 'RTGI\\Shaders\\qUINT_rtgi_diffuse.fx'
lineNumber2 = 32
text2 = " #define SKYCOLOR_MODE          3   //[0 to 3]      0: skycolor feature disabled | 1: manual skycolor | 2: dynamic skycolor | 3: dynamic skycolor with manual tint overlay"
    
with open(fileName2) as file:
    lines = file.readlines()

lines[lineNumber2 - 1] = text2 + "\n"

with open(fileName2, 'w') as file:
    for line in lines:
        file.write(line)
print("Set pre-processor definition")


#File modifications 3
fileName3 = 'RTGI\\Shaders\\qUINT_rtgi_diffuse.fx'
lineNumber3 = 36
text3 = " #define IMAGEBASEDLIGHTING     1   //[0 to 3]      0: no ibl infill | 1: use ibl infill"
    
with open(fileName3) as file:
    lines = file.readlines()

lines[lineNumber3 - 1] = text3 + "\n"

with open(fileName3, 'w') as file:
    for line in lines:
        file.write(line)
print("Set pre-processor definition")


#File modifications 4
fileName4 = 'RTGI\\Shaders\\qUINT_rtgi_diffuse.fx'
lineNumber4 = 40
text4 = " #define MATERIAL_TYPE          1   //[0 to 1]      0: Lambert diffuse | 1: GGX BRDF"
    
with open(fileName4) as file:
    lines = file.readlines()

lines[lineNumber4 - 1] = text4 + "\n"

with open(fileName4, 'w') as file:
    for line in lines:
        file.write(line)
print("Set pre-processor definition")


#File modifications 5
fileName5 = 'RTGI\\Shaders\\qUINT_rtgi_diffuse.fx'
lineNumber5 = 44
text5 = " #define SMOOTHNORMALS 			1   //[0 to 3]      0: off | 1: enables some filtering of the normals derived from depth buffer to hide 3d model blockyness"
    
with open(fileName5) as file:
    lines = file.readlines()

lines[lineNumber5 - 1] = text5 + "\n"

with open(fileName5, 'w') as file:
    for line in lines:
        file.write(line)
print("Set pre-processor definition")


#File modifications 6
fileName6 = 'RTGI\\Shaders\\qUINT_rtgi_diffuse.fx'
lineNumber6 = 44
text6 = " #define SMOOTHNORMALS 			1   //[0 to 3]      0: off | 1: enables some filtering of the normals derived from depth buffer to hide 3d model blockyness"
    
with open(fileName6) as file:
    lines = file.readlines()

lines[lineNumber6 - 1] = text6 + "\n"

with open(fileName6, 'w') as file:
    for line in lines:
        file.write(line)
print("Set pre-processor definition")

#Pre-processor definitions end



#Change bluenoise file name to the new one

#File modifications 7
fileName7 = 'RTGI\\Shaders\\qUINT_rtgi_diffuse.fx'
lineNumber7 = 361
text7 = 'texture JitterTex       < source = "rtgibluenoise.png"; > { Width = 32; Height = 32; Format = RGBA8; };'
    
with open(fileName7) as file:
    lines = file.readlines()

lines[lineNumber7 - 1] = text7 + "\n"

with open(fileName7, 'w') as file:
    for line in lines:
        file.write(line)
print("Changed bluenoise file name to the new one")



#Setting default settings

#File modifications 8
fileName8 = 'RTGI\\Shaders\\qUINT_rtgi_diffuse.fx'
lineNumber8 = 61
text8 = "	ui_min = 0.5; ui_max = 50.0;"
    
with open(fileName8) as file:
    lines = file.readlines()

lines[lineNumber8 - 1] = text8 + "\n"

with open(fileName8, 'w') as file:
    for line in lines:
        file.write(line)
print("Changed settings")


#File modifications 9
fileName9 = 'RTGI\\Shaders\\qUINT_rtgi_diffuse.fx'
lineNumber9 = 66
text9 = "> = 50.0;"
    
with open(fileName9) as file:
    lines = file.readlines()

lines[lineNumber9 - 1] = text9 + "\n"

with open(fileName9, 'w') as file:
    for line in lines:
        file.write(line)
print("Changed settings")


#File modifications 10
fileName10 = 'RTGI\\Shaders\\qUINT_rtgi_diffuse.fx'
lineNumber10 = 70
text10 = "	ui_min = 0.0; ui_max = 10.0;"
    
with open(fileName10) as file:
    lines = file.readlines()

lines[lineNumber10 - 1] = text10 + "\n"

with open(fileName10, 'w') as file:
    for line in lines:
        file.write(line)
print("Changed settings")


#File modifications 11
# fileName11 = 'RTGI\\Shaders\\qUINT_rtgi_diffuse.fx'
# lineNumber11 = 70
# text11 = "	ui_min = 0.0; ui_max = 10.0;"
#     
# with open(fileName11) as file:
#     lines = file.readlines()
#
# lines[lineNumber11 - 1] = text11 + "\n"
#
# with open(fileName11, 'w') as file:
#     for line in lines:
#         file.write(line)
# print("Changed settings")


#File modifications 12
fileName12 = 'RTGI\\Shaders\\qUINT_rtgi_diffuse.fx'
lineNumber12 = 75
text12 = "> = 10.0;"
    
with open(fileName12) as file:
    lines = file.readlines()

lines[lineNumber12 - 1] = text12 + "\n"

with open(fileName12, 'w') as file:
    for line in lines:
        file.write(line)
print("Changed settings")


#File modifications 13
fileName13 = 'RTGI\\Shaders\\qUINT_rtgi_diffuse.fx'
lineNumber13 = 79
text13 = "	ui_min = 1; ui_max = 500;"
    
with open(fileName13) as file:
    lines = file.readlines()

lines[lineNumber13 - 1] = text13 + "\n"

with open(fileName13, 'w') as file:
    for line in lines:
        file.write(line)
print("Changed settings")


#File modifications 14
fileName14 = 'RTGI\\Shaders\\qUINT_rtgi_diffuse.fx'
lineNumber14 = 83
text14 = "> = 3;"
    
with open(fileName14) as file:
    lines = file.readlines()

lines[lineNumber14 - 1] = text14 + "\n"

with open(fileName14, 'w') as file:
    for line in lines:
        file.write(line)
print("Changed settings")


#File modifications 15
fileName15 = 'RTGI\\Shaders\\qUINT_rtgi_diffuse.fx'
lineNumber15 = 87
text15 = "	ui_min = 1; ui_max = 255;"
    
with open(fileName15) as file:
    lines = file.readlines()

lines[lineNumber15 - 1] = text15 + "\n"

with open(fileName15, 'w') as file:
    for line in lines:
        file.write(line)
print("Changed settings")


#File modifications 16
fileName16 = 'RTGI\\Shaders\\qUINT_rtgi_diffuse.fx'
lineNumber16 = 91
text16 = "> = 55;"
    
with open(fileName16) as file:
    lines = file.readlines()

lines[lineNumber16 - 1] = text16 + "\n"

with open(fileName16, 'w') as file:
    for line in lines:
        file.write(line)
print("Changed settings")


#File modifications 17
fileName17 = 'RTGI\\Shaders\\qUINT_rtgi_diffuse.fx'
lineNumber17 = 100
text17 = "> = 0.08;"
    
with open(fileName17) as file:
    lines = file.readlines()

lines[lineNumber17 - 1] = text17 + "\n"

with open(fileName17, 'w') as file:
    for line in lines:
        file.write(line)
print("Changed settings")


#File modifications 18
fileName18 = 'RTGI\\Shaders\\qUINT_rtgi_diffuse.fx'
lineNumber18 = 111
text18 = "	ui_min = -0.05; ui_max = 1.0;"
    
with open(fileName18) as file:
    lines = file.readlines()

lines[lineNumber18 - 1] = text18 + "\n"

with open(fileName18, 'w') as file:
    for line in lines:
        file.write(line)
print("Changed settings")


#File modifications 19
fileName19 = 'RTGI\\Shaders\\qUINT_rtgi_diffuse.fx'
lineNumber19 = 116
text19 = "> = 1.0;"
    
with open(fileName19) as file:
    lines = file.readlines()

lines[lineNumber19 - 1] = text19 + "\n"

with open(fileName19, 'w') as file:
    for line in lines:
        file.write(line)
print("Changed settings")


#File modifications 20
fileName20 = 'RTGI\\Shaders\\qUINT_rtgi_diffuse.fx'
lineNumber20 = 121
text20 = "	ui_min = -0.05; ui_max = 2.0;"
    
with open(fileName20) as file:
    lines = file.readlines()

lines[lineNumber20 - 1] = text20 + "\n"

with open(fileName20, 'w') as file:
    for line in lines:
        file.write(line)
print("Changed settings")


#File modifications 21
fileName21 = 'RTGI\\Shaders\\qUINT_rtgi_diffuse.fx'
lineNumber21 = 125
text21 = "> = 0.1;"
    
with open(fileName21) as file:
    lines = file.readlines()

lines[lineNumber21 - 1] = text21 + "\n"

with open(fileName21, 'w') as file:
    for line in lines:
        file.write(line)
print("Changed settings")


#File modifications 22
fileName22 = 'RTGI\\Shaders\\qUINT_rtgi_diffuse.fx'
lineNumber22 = 161
text22 = "> = 0.0;"
    
with open(fileName22) as file:
    lines = file.readlines()

lines[lineNumber22 - 1] = text22 + "\n"

with open(fileName22, 'w') as file:
    for line in lines:
        file.write(line)
print("Changed settings")


#File modifications 23
fileName23 = 'RTGI\\Shaders\\qUINT_rtgi_diffuse.fx'
lineNumber23 = 169
text23 = "> = 0.0;"
    
with open(fileName23) as file:
    lines = file.readlines()

lines[lineNumber23 - 1] = text23 + "\n"

with open(fileName23, 'w') as file:
    for line in lines:
        file.write(line)
print("Changed settings")


#File modifications 24
fileName24 = 'RTGI\\Shaders\\qUINT_rtgi_diffuse.fx'
lineNumber24 = 178
text24 = "> = 0.5;"
    
with open(fileName24) as file:
    lines = file.readlines()

lines[lineNumber24 - 1] = text24 + "\n"

with open(fileName24, 'w') as file:
    for line in lines:
        file.write(line)
print("Changed settings")


#File modifications 25
fileName25 = 'RTGI\\Shaders\\qUINT_rtgi_diffuse.fx'
lineNumber25 = 186
text25 = "> = 2.0;"
    
with open(fileName25) as file:
    lines = file.readlines()

lines[lineNumber25 - 1] = text25 + "\n"

with open(fileName25, 'w') as file:
    for line in lines:
        file.write(line)
print("Changed settings")


#File modifications 26
fileName26 = 'RTGI\\Shaders\\qUINT_rtgi_diffuse.fx'
lineNumber26 = 195
text26 = "> = 0.0;"
    
with open(fileName26) as file:
    lines = file.readlines()

lines[lineNumber26 - 1] = text26 + "\n"

with open(fileName26, 'w') as file:
    for line in lines:
        file.write(line)
print("Changed settings")


#File modifications 26
fileName26 = 'RTGI\\Shaders\\qUINT_rtgi_diffuse.fx'
lineNumber26 = 205
text26 = "> = 0.25;"
    
with open(fileName26) as file:
    lines = file.readlines()

lines[lineNumber26 - 1] = text26 + "\n"

with open(fileName26, 'w') as file:
    for line in lines:
        file.write(line)
print("Changed settings")


#File modifications 27
#fileName27 = 'RTGI\\Shaders\\qUINT_rtgi_diffuse.fx'
#lineNumber27 = 218
#text27 = "	ui_min = 0.001; ui_max = 10.0;"
#    
#with open(fileName27) as file:
#    lines = file.readlines()
#
#lines[lineNumber27 - 1] = text27 + "\n"
#
#with open(fileName27, 'w') as file:
#    for line in lines:
#        file.write(line)
#print("Changed settings")


#File modifications 28
fileName28 = 'RTGI\\Shaders\\qUINT_rtgi_diffuse.fx'
lineNumber28 = 221
text28 = "> = 1.0;"
    
with open(fileName28) as file:
    lines = file.readlines()

lines[lineNumber28 - 1] = text28 + "\n"

with open(fileName28, 'w') as file:
    for line in lines:
        file.write(line)
print("Changed settings")



# Duplicate and rename rtgi file

shutil.copy2('RTGI\\Shaders\\qUINT_rtgi_diffuse.fx','RTGI\\Shaders\\qUINT_rtgi_specular.fx')
print("Duplicated and renamed file")


#Change roughness of specular

#File modifications 29
fileName29 = 'RTGI\\Shaders\\qUINT_rtgi_specular.fx'
lineNumber29 = 116
text29 = "> = 0.5;"
    
with open(fileName29) as file:
    lines = file.readlines()

lines[lineNumber29 - 1] = text29 + "\n"

with open(fileName29, 'w') as file:
    for line in lines:
        file.write(line)
print("Changed roughness of specular")

#Make folder to put files in
newpath = r"PUT IN ANSEL"
os.makedirs(newpath)
print("Made folder to put files in")

shutil.move('RTGI\\Shaders\\qUINT_rtgi_diffuse.fx', 'PUT IN ANSEL\\qUINT_rtgi_diffuse.fx')
print("Moved diffuse")
shutil.move('RTGI\\Shaders\\qUINT_rtgi_specular.fx', 'PUT IN ANSEL\\qUINT_rtgi_specular.fx')
print("Moved specular")
shutil.move('RTGI\\Shaders\\qUINT', 'PUT IN ANSEL\\qUINT')
print("Moved qUINT folder")
shutil.move('RTGI\\Textures\\rtgibluenoise.png', 'PUT IN ANSEL\\rtgibluenoise.png')
print("Moved rtgibluenoise.png")

# Move files to the Ansel folder
# Had to be removed due to windows defender being windows defender yk

#shutil.move('RTGI\\Shaders\\qUINT_rtgi_diffuse.fx', 'C:\\Program Files\\NVIDIA Corporation\\Ansel\\qUINT_rtgi_diffuse.fx')
#print("Moved diffuse")
#shutil.move('RTGI\\Shaders\\qUINT_rtgi_specular.fx', 'C:\\Program Files\\NVIDIA Corporation\\Ansel\\qUINT_rtgi_specular.fx')
#print("Moved specular")
#shutil.move('RTGI\\Shaders\\qUINT', 'C:\\Program Files\\NVIDIA Corporation\\Ansel\\qUINT')
#print("Moved qUINT folder")
#shutil.move('RTGI\\Textures\\rtgibluenoise.png', 'C:\\Program Files\\NVIDIA Corporation\\Ansel\\rtgibluenoise.png')
#print("Moved rtgibluenoise.png")

#Delete the RTGI directory
shutil.rmtree('RTGI')

print("")
print("")
print("")

print(r"Press Win + R and put C:\Program Files\NVIDIA Corporation\Ansel and hit enter.")
print("Then move all files from PUT IN ANSEL to this directory.")
print("")

input("Press enter when you're done. ")

shutil.rmtree('PUT IN ANSEL')

#print("Successfully installed qUINT_rtgi.")

#MessageBox = ctypes.windll.user32.MessageBoxW
#MessageBox(None, 'Successfully installed qUINT_rtgi.', 'rtgiSetup.exe', 0)