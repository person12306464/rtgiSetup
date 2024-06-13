using System.Security.Principal;

#pragma warning disable CA1416 // Validate platform compatibility
bool isAdmin = new WindowsPrincipal(WindowsIdentity.GetCurrent()).IsInRole(WindowsBuiltInRole.Administrator);
#pragma warning restore CA1416 // Validate platform compatibility

if (isAdmin == false)
{
    Console.WriteLine("This program needs to be run as admin.");
    Console.WriteLine("It needs this to add files to the Ansel folder.");
    Console.Write("Press Enter to close this. ");
    while (Console.ReadKey().Key != ConsoleKey.Enter) { }
    Environment.Exit(1);
}


string root = AppDomain.CurrentDomain.BaseDirectory;


bool rtgiExists = File.Exists(root + "ReShade_GI_Beta_0.36.1.zip");

if (rtgiExists == false)
{
    Console.WriteLine("Could not find the rtgi version.");
    Console.WriteLine("Make sure it's 0.36.1 and has the default name.");
    Console.Write("Press Enter to close this. ");
    while (Console.ReadKey().Key != ConsoleKey.Enter) { }
    Environment.Exit(2);
}
Console.WriteLine("Found rtgi");

bool extractionFolderExists = Directory.Exists(root + "RTGI");
if (extractionFolderExists == true)
{
    Directory.Delete(root + "RTGI", true);
    Console.WriteLine("Deleted old extraction folder");
}

string zipPath = root + "ReShade_GI_Beta_0.36.1.zip";
string extractPath = root + "RTGI";

System.IO.Compression.ZipFile.ExtractToDirectory(zipPath, extractPath);
Console.WriteLine("Extracted rtgi");


// Rename files
File.Move(root + @"RTGI\Shaders\qUINT_rtgi.fx", root + @"RTGI\Shaders\qUINT_rtgi_diffuse.fx");
Console.WriteLine("Renamed rtgi");

File.Move(root + @"RTGI\Textures\bluenoise.png", root + @"RTGI\Textures\rtgibluenoise.png");
Console.WriteLine("Renamed bluenoise");


static void lineChanger(string newText, string fileName, int line_to_edit)
{
    string[] arrLine = File.ReadAllLines(fileName);
    arrLine[line_to_edit - 1] = newText;
    File.WriteAllLines(fileName, arrLine);
}

// Delete Reshade version check
lineChanger("", root + @"RTGI\Shaders\qUINT_rtgi_diffuse.fx", 19);
lineChanger("", root + @"RTGI\Shaders\qUINT_rtgi_diffuse.fx", 20);
lineChanger("", root + @"RTGI\Shaders\qUINT_rtgi_diffuse.fx", 21);
Console.WriteLine("Removed Reshade version check");


// Set pre-processor definitions
lineChanger(" #define INFINITE_BOUNCES       1   //[0 or 1]      If enabled, path tracer samples previous frame GI as well, causing a feedback loop to simulate secondary bounces, causing a more widespread GI.", root + @"RTGI\Shaders\qUINT_rtgi_diffuse.fx", 28);
Console.WriteLine("Set pre-processor definition");

lineChanger(" #define SKYCOLOR_MODE          3   //[0 to 3]      0: skycolor feature disabled | 1: manual skycolor | 2: dynamic skycolor | 3: dynamic skycolor with manual tint overlay", root + @"RTGI\Shaders\qUINT_rtgi_diffuse.fx", 32);
Console.WriteLine("Set pre-processor definition");

lineChanger(" #define IMAGEBASEDLIGHTING     1   //[0 to 3]      0: no ibl infill | 1: use ibl infill", root + @"RTGI\Shaders\qUINT_rtgi_diffuse.fx", 36);
Console.WriteLine("Set pre-processor definition");

lineChanger(" #define MATERIAL_TYPE          1   //[0 to 1]      0: Lambert diffuse | 1: GGX BRDF", root + @"RTGI\Shaders\qUINT_rtgi_diffuse.fx", 40);
Console.WriteLine("Set pre-processor definition");

lineChanger(" #define SMOOTHNORMALS 			1   //[0 to 3]      0: off | 1: enables some filtering of the normals derived from depth buffer to hide 3d model blockyness", root + @"RTGI\Shaders\qUINT_rtgi_diffuse.fx", 44);
Console.WriteLine("Set pre-processor definition");


// Change bluenoise file name to the new one
lineChanger("""texture JitterTex       < source = "rtgibluenoise.png"; > { Width = 32; Height = 32; Format = RGBA8; };""", root + @"RTGI\Shaders\qUINT_rtgi_diffuse.fx", 361);
Console.WriteLine("Changed bluenoise file name to the new one");


// Setting default settings
lineChanger("	ui_min = 0.5; ui_max = 50.0;", root + @"RTGI\Shaders\qUINT_rtgi_diffuse.fx", 61);
Console.WriteLine("Changed settings");

lineChanger("> = 50.0;", root + @"RTGI\Shaders\qUINT_rtgi_diffuse.fx", 66);
Console.WriteLine("Changed settings");

lineChanger("	ui_min = 0.0; ui_max = 10.0;", root + @"RTGI\Shaders\qUINT_rtgi_diffuse.fx", 70);
Console.WriteLine("Changed settings");

lineChanger("> = 10.0;", root + @"RTGI\Shaders\qUINT_rtgi_diffuse.fx", 75);
Console.WriteLine("Changed settings");

lineChanger("	ui_min = 1; ui_max = 500;", root + @"RTGI\Shaders\qUINT_rtgi_diffuse.fx", 79);
Console.WriteLine("Changed settings");

lineChanger("> = 3;", root + @"RTGI\Shaders\qUINT_rtgi_diffuse.fx", 83);
Console.WriteLine("Changed settings");

lineChanger("	ui_min = 1; ui_max = 255;", root + @"RTGI\Shaders\qUINT_rtgi_diffuse.fx", 87);
Console.WriteLine("Changed settings");

lineChanger("> = 55;", root + @"RTGI\Shaders\qUINT_rtgi_diffuse.fx", 91);
Console.WriteLine("Changed settings");

lineChanger("> = 0.12;", root + @"RTGI\Shaders\qUINT_rtgi_diffuse.fx", 100);
Console.WriteLine("Changed settings");

lineChanger("	ui_min = -0.05; ui_max = 1.0;", root + @"RTGI\Shaders\qUINT_rtgi_diffuse.fx", 111);
Console.WriteLine("Changed settings");

lineChanger("> = 1.0;", root + @"RTGI\Shaders\qUINT_rtgi_diffuse.fx", 116);
Console.WriteLine("Changed settings");

lineChanger("	ui_min = -0.05; ui_max = 2.0;", root + @"RTGI\Shaders\qUINT_rtgi_diffuse.fx", 121);
Console.WriteLine("Changed settings");

lineChanger("> = 0.1;", root + @"RTGI\Shaders\qUINT_rtgi_diffuse.fx", 125);
Console.WriteLine("Changed settings");

lineChanger("> = 0.0;", root + @"RTGI\Shaders\qUINT_rtgi_diffuse.fx", 161);
Console.WriteLine("Changed settings");

lineChanger("> = 0.0;", root + @"RTGI\Shaders\qUINT_rtgi_diffuse.fx", 169);
Console.WriteLine("Changed settings");

lineChanger("> = 0.5;", root + @"RTGI\Shaders\qUINT_rtgi_diffuse.fx", 178);
Console.WriteLine("Changed settings");

lineChanger("> = 2.0;", root + @"RTGI\Shaders\qUINT_rtgi_diffuse.fx", 186);
Console.WriteLine("Changed settings");

lineChanger("> = 0.0;", root + @"RTGI\Shaders\qUINT_rtgi_diffuse.fx", 195);
Console.WriteLine("Changed settings");

lineChanger("> = 0.25;", root + @"RTGI\Shaders\qUINT_rtgi_diffuse.fx", 205);
Console.WriteLine("Changed settings");

lineChanger("> = 1.0;", root + @"RTGI\Shaders\qUINT_rtgi_diffuse.fx", 221);
Console.WriteLine("Changed settings");


// Duplicate and rename rtgi file
File.Copy(root + @"RTGI\Shaders\qUINT_rtgi_diffuse.fx", root + @"RTGI\Shaders\qUINT_rtgi_specular.fx");
Console.WriteLine("Duplicated and renamed rtgi file");


// Change roughness of specular
lineChanger("> = 0.5;", root + @"RTGI\Shaders\qUINT_rtgi_specular.fx", 116);
Console.WriteLine("Changed roughness of specular");


bool diffuseExists = File.Exists(@"C:\Program Files\NVIDIA Corporation\Ansel\qUINT_rtgi_diffuse.fx");
bool specularExists = File.Exists(@"C:\Program Files\NVIDIA Corporation\Ansel\qUINT_rtgi_specular.fx");
bool quintExists = Directory.Exists(@"C:\Program Files\NVIDIA Corporation\Ansel\qUINT");
bool bluenoiseExists = File.Exists(@"C:\Program Files\NVIDIA Corporation\Ansel\rtgibluenoise.png");

if (diffuseExists == true)
{
    File.Delete(@"C:\Program Files\NVIDIA Corporation\Ansel\qUINT_rtgi_diffuse.fx");
    Console.WriteLine("Deleted old diffuse");
}

if (specularExists == true)
{
    File.Delete(@"C:\Program Files\NVIDIA Corporation\Ansel\qUINT_rtgi_specular.fx");
    Console.WriteLine("Deleted old specular");
}

if (quintExists == true)
{
    Directory.Delete(@"C:\Program Files\NVIDIA Corporation\Ansel\qUINT", true);
    Console.WriteLine("Deleted old qUINT folder");
}

if (bluenoiseExists == true)
{
    File.Delete(@"C:\Program Files\NVIDIA Corporation\Ansel\rtgibluenoise.png");
    Console.WriteLine("Deleted old bluenoise");
}

File.Move(root + @"RTGI\Shaders\qUINT_rtgi_diffuse.fx", @"C:\Program Files\NVIDIA Corporation\Ansel\qUINT_rtgi_diffuse.fx");
Console.WriteLine("Moved diffuse");
File.Move(root + @"RTGI\Shaders\qUINT_rtgi_specular.fx", @"C:\Program Files\NVIDIA Corporation\Ansel\qUINT_rtgi_specular.fx");
Console.WriteLine("Moved specular");
Directory.Move(root + @"RTGI\Shaders\qUINT", @"C:\Program Files\NVIDIA Corporation\Ansel\qUINT");
Console.WriteLine("Moved qUINT folder");
File.Move(root + @"RTGI\Textures\rtgibluenoise.png", @"C:\Program Files\NVIDIA Corporation\Ansel\rtgibluenoise.png");
Console.WriteLine("Moved rtgibluenoise.png");


Directory.Delete(root + "RTGI", true);



Console.WriteLine("");
Console.WriteLine("");
Console.WriteLine("");
Console.WriteLine("Successfully installed qUINT_rtgi.");
Console.WriteLine("Press Enter to close this. ");
while (Console.ReadKey().Key != ConsoleKey.Enter) { }