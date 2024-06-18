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

//lineChanger(" #define SKYCOLOR_MODE          3   //[0 to 3]      0: skycolor feature disabled | 1: manual skycolor | 2: dynamic skycolor | 3: dynamic skycolor with manual tint overlay", root + @"RTGI\Shaders\qUINT_rtgi_diffuse.fx", 32);
//Console.WriteLine("Set pre-processor definition");

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

lineChanger("	ui_min = 0.0; ui_max = 1.0;", root + @"RTGI\Shaders\qUINT_rtgi_diffuse.fx", 111);
Console.WriteLine("Changed settings");

lineChanger("> = 1.0;", root + @"RTGI\Shaders\qUINT_rtgi_diffuse.fx", 116);
Console.WriteLine("Changed settings");

lineChanger("	ui_min = 0.0; ui_max = 1.0;", root + @"RTGI\Shaders\qUINT_rtgi_diffuse.fx", 121);
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

lineChanger("""    ui_label = "Render a still frame (for screenshots)";""", root + @"RTGI\Shaders\qUINT_rtgi_diffuse.fx", 232);
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



bool ptgiExists = File.Exists(root + "ReShade_GI_Beta_0.17.0.2.zip");

Console.WriteLine("\n\n");

if (ptgiExists == false)
{
    Console.WriteLine("Did not find ptgi");
    Console.WriteLine("");
}

Console.WriteLine("Successfully installed qUINT_rtgi.");



if (ptgiExists == true)
{
    Console.WriteLine("\nFound ptgi");

    bool extractionFolderExists2 = Directory.Exists(root + "PTGI");
    if (extractionFolderExists2 == true)
    {
        Directory.Delete(root + "PTGI", true);
        Console.WriteLine("Deleted old extraction folder");
    }


    string zipPath2 = root + "ReShade_GI_Beta_0.17.0.2.zip";
    string extractPath2 = root + "PTGI";

    System.IO.Compression.ZipFile.ExtractToDirectory(zipPath2, extractPath2);
    Console.WriteLine("Extracted ptgi");

    // Rename files
    File.Move(root + @"PTGI\Shaders\qUINT_rtgi.fx", root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx");
    Console.WriteLine("Renamed ptgi");

    File.Move(root + @"PTGI\Shaders\qUINT_common.fxh", root + @"PTGI\Shaders\qUINT_ptgi_common.fxh");
    Console.WriteLine("Renamed qUINT_common");


    // New lines (New line gets created under the line specified)

    var allLines = File.ReadAllLines(root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx").ToList();
    allLines.Insert(204, "");
    File.WriteAllLines(root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", allLines.ToArray());
    Console.WriteLine("Added line");

    var allLines2 = File.ReadAllLines(root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx").ToList();
    allLines2.Insert(205, "");
    File.WriteAllLines(root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", allLines2.ToArray());
    Console.WriteLine("Added line");

    var allLines3 = File.ReadAllLines(root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx").ToList();
    allLines3.Insert(206, "");
    File.WriteAllLines(root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", allLines3.ToArray());
    Console.WriteLine("Added line");

    var allLines4 = File.ReadAllLines(root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx").ToList();
    allLines3.Insert(207, "");
    File.WriteAllLines(root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", allLines4.ToArray());
    Console.WriteLine("Added line");

    var allLines5 = File.ReadAllLines(root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx").ToList();
    allLines5.Insert(208, "");
    File.WriteAllLines(root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", allLines5.ToArray());
    Console.WriteLine("Added line");

    var allLines6 = File.ReadAllLines(root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx").ToList();
    allLines6.Insert(209, "");
    File.WriteAllLines(root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", allLines6.ToArray());
    Console.WriteLine("Added line");

    var allLines7 = File.ReadAllLines(root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx").ToList();
    allLines7.Insert(210, "");
    File.WriteAllLines(root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", allLines7.ToArray());
    Console.WriteLine("Added line");

    // This line adder thing kinda sucks and is very weird but it works kind of ¯\_(ツ)_/¯

    // Adding stuff to the lines

    lineChanger("uniform bool RT_DO_RENDER <", root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", 211);
    Console.WriteLine("Changed stuff");

    lineChanger("uniform bool RT_DO_RENDER_SCREENSHOT <", root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", 205);
    Console.WriteLine("Changed stuff");

    lineChanger("""    ui_label = "Render a still frame (for screenshots)";""", root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", 206);
    Console.WriteLine("Changed stuff");

    lineChanger("""    ui_category = "Experimental";""", root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", 207);
    Console.WriteLine("Changed stuff");

    lineChanger("""    ui_tooltip = "This will progressively render a still frame with extreme quality (and filter off, for now).\nTo start rendering, check the box and wait until the result is sufficiently noise-free.\nYou can still adjust blending and toggle debug mode, but do not touch anything else.\nTo resume the game, uncheck the box.\n\nRequires a scene with no moving objects to work properly.";""", root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", 208);
    Console.WriteLine("Changed stuff");

    lineChanger("> = false;", root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", 209);
    Console.WriteLine("Changed stuff");


    lineChanger("", root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", 210);
    Console.WriteLine("Changed stuff");

    lineChanger("uniform bool RT_DO_RENDER <", root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", 211);
    Console.WriteLine("Changed stuff");

    lineChanger("""    ui_label = "Real Time Render";""", root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", 212);
    Console.WriteLine("Changed stuff");

    lineChanger("> = true;", root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", 215);
    Console.WriteLine("Changed stuff");


    // Stuff for the screenshot mode

    var allLines8 = File.ReadAllLines(root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx").ToList();
    allLines8.Insert(621, "");
    File.WriteAllLines(root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", allLines8.ToArray());
    Console.WriteLine("Added line");

    var allLines9 = File.ReadAllLines(root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx").ToList();
    allLines9.Insert(622, "");
    File.WriteAllLines(root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", allLines9.ToArray());
    Console.WriteLine("Added line");

    var allLines10 = File.ReadAllLines(root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx").ToList();
    allLines10.Insert(623, "");
    File.WriteAllLines(root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", allLines10.ToArray());
    Console.WriteLine("Added line");

    var allLines11 = File.ReadAllLines(root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx").ToList();
    allLines11.Insert(624, "");
    File.WriteAllLines(root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", allLines11.ToArray());
    Console.WriteLine("Added line");

    var allLines12 = File.ReadAllLines(root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx").ToList();
    allLines12.Insert(625, "");
    File.WriteAllLines(root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", allLines12.ToArray());
    Console.WriteLine("Added line");

    var allLines13 = File.ReadAllLines(root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx").ToList();
    allLines13.Insert(626, "");
    File.WriteAllLines(root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", allLines13.ToArray());
    Console.WriteLine("Added line");

    var allLines14 = File.ReadAllLines(root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx").ToList();
    allLines14.Insert(627, "");
    File.WriteAllLines(root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", allLines14.ToArray());
    Console.WriteLine("Added line");

    var allLines15 = File.ReadAllLines(root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx").ToList();
    allLines15.Insert(628, "");
    File.WriteAllLines(root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", allLines15.ToArray());
    Console.WriteLine("Added line");

    var allLines16 = File.ReadAllLines(root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx").ToList();
    allLines16.Insert(629, "");
    File.WriteAllLines(root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", allLines16.ToArray());
    Console.WriteLine("Added line");

    var allLines17 = File.ReadAllLines(root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx").ToList();
    allLines17.Insert(630, "");
    File.WriteAllLines(root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", allLines17.ToArray());
    Console.WriteLine("Added line");

    var allLines18 = File.ReadAllLines(root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx").ToList();
    allLines18.Insert(631, "");
    File.WriteAllLines(root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", allLines18.ToArray());
    Console.WriteLine("Added line");


    // All lines are now added and I'll add the code to those lines

    lineChanger("int RT_DO_RENDER_REAL_TIME;", root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", 622);
    Console.WriteLine("Changed stuff");

    lineChanger("if (RT_DO_RENDER_SCREENSHOT == true)", root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", 624);
    Console.WriteLine("Changed stuff");

    lineChanger("{", root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", 625);
    Console.WriteLine("Changed stuff");

    lineChanger("    RT_DO_RENDER_REAL_TIME = 0;", root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", 626);
    Console.WriteLine("Changed stuff");

    lineChanger("}", root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", 627);
    Console.WriteLine("Changed stuff");

    lineChanger("else", root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", 628);
    Console.WriteLine("Changed stuff");

    lineChanger("{", root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", 629);
    Console.WriteLine("Changed stuff");

    lineChanger("    RT_DO_RENDER_REAL_TIME = 1;", root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", 630);
    Console.WriteLine("Changed stuff");

    lineChanger("}", root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", 631);
    Console.WriteLine("Changed stuff");

    lineChanger("#define read_counter(tex) tex2Dfetch(tex, RT_DO_RENDER_REAL_TIME).w", root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", 633);
    Console.WriteLine("Changed stuff");


    // Make ray and steps option affect do render

    lineChanger("        o.nsteps  = RT_RAY_AMOUNT;", root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", 357);
    Console.WriteLine("Changed stuff");

    lineChanger("        o.nrays   = RT_RAY_STEPS;", root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", 358);
    Console.WriteLine("Changed stuff");


    // Set new qUINT common file name

    lineChanger("#include \"qUINT_ptgi_common.fxh\"", root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", 243);
    Console.WriteLine("Changed stuff");


    // Set pre-processor definitions

    lineChanger(" #define INFINITE_BOUNCES       1   //[0 or 1]      If enabled, path tracer samples previous frame GI as well, causing a feedback loop to simulate secondary bounces, causing a more widespread GI.", root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", 22);
    Console.WriteLine("Set pre-processor definition");

    lineChanger(" #define MATERIAL_TYPE          1   //[0 to 1]      0: Lambert diffuse | 1: GGX BRDF", root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", 30);
    Console.WriteLine("Set pre-processor definition");


    // Change bluenoise file name to the new one

    lineChanger("""texture JitterTex < source = "rtgibluenoise.png"; > { Width = 32; 			  Height = 32; 				Format = RGBA8; };""", root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", 275);
    Console.WriteLine("Changed bluenoise file name to the new one");


    // Setting default settings

    lineChanger("	ui_min = 0.5; ui_max = 50.0;", root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", 55);
    Console.WriteLine("Changed settings");

    lineChanger("> = 20;", root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", 60);
    Console.WriteLine("Changed settings");

    lineChanger("	ui_min = 1; ui_max = 500;", root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", 64);
    Console.WriteLine("Changed settings");

    lineChanger("> = 20;", root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", 68);
    Console.WriteLine("Changed settings");

    lineChanger("	ui_min = 1; ui_max = 255;", root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", 72);
    Console.WriteLine("Changed settings");

    lineChanger("> = 40;", root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", 76);
    Console.WriteLine("Changed settings");

    lineChanger("> = 0.12;", root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", 85);
    Console.WriteLine("Changed settings");

    lineChanger("> = true;", root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", 97);
    Console.WriteLine("Changed settings");

    lineChanger("	ui_min = 0.01; ui_max = 5.0;", root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", 108);
    Console.WriteLine("Changed settings");

    lineChanger("	ui_min = 0.0; ui_max = 1.0;", root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", 117);
    Console.WriteLine("Changed settings");

    lineChanger("> = 0.0;", root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", 152);
    Console.WriteLine("Changed settings");

    lineChanger("> = 0.0;", root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", 160);
    Console.WriteLine("Changed settings");

    lineChanger("> = 10.0;", root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", 169);
    Console.WriteLine("Changed settings");

    lineChanger("> = 5.5;", root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", 177);
    Console.WriteLine("Changed settings");

    lineChanger("> = float2(0.5, 1.0);", root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", 195);
    Console.WriteLine("Changed settings");


    // Duplicate and rename ptgi file
    File.Copy(root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", root + @"PTGI\Shaders\qUINT_ptgi_specular.fx");
    Console.WriteLine("Duplicated and renamed ptgi file");


    // Change roughness of specular
    lineChanger("> = 0.5;", root + @"PTGI\Shaders\qUINT_ptgi_specular.fx", 122);
    Console.WriteLine("Changed roughness of specular");



    bool ptgiDiffuseExists = File.Exists(@"C:\Program Files\NVIDIA Corporation\Ansel\qUINT_ptgi_diffuse.fx");
    bool ptgiSpecularExists = File.Exists(@"C:\Program Files\NVIDIA Corporation\Ansel\qUINT_ptgi_specular.fx");
    bool ptgiQuintExists = Directory.Exists(@"C:\Program Files\NVIDIA Corporation\Ansel\RTGI");
    bool ptgiQuintCommonExists = File.Exists(@"C:\Program Files\NVIDIA Corporation\Ansel\qUINT_ptgi_common.fxh");

    if (ptgiDiffuseExists == true)
    {
        File.Delete(@"C:\Program Files\NVIDIA Corporation\Ansel\qUINT_ptgi_diffuse.fx");
        Console.WriteLine("Deleted old diffuse");
    }

    if (ptgiSpecularExists == true)
    {
        File.Delete(@"C:\Program Files\NVIDIA Corporation\Ansel\qUINT_ptgi_specular.fx");
        Console.WriteLine("Deleted old specular");
    }

    if (ptgiQuintExists == true)
    {
        Directory.Delete(@"C:\Program Files\NVIDIA Corporation\Ansel\RTGI", true);
        Console.WriteLine("Deleted old RTGI (ptgi) folder");
    }

    if (ptgiQuintCommonExists == true)
    {
        File.Delete(@"C:\Program Files\NVIDIA Corporation\Ansel\qUINT_ptgi_common.fxh");
        Console.WriteLine("Deleted old qUINT common");
    }


    File.Move(root + @"PTGI\Shaders\qUINT_ptgi_diffuse.fx", @"C:\Program Files\NVIDIA Corporation\Ansel\qUINT_ptgi_diffuse.fx");
    Console.WriteLine("Moved diffuse");
    File.Move(root + @"PTGI\Shaders\qUINT_ptgi_specular.fx", @"C:\Program Files\NVIDIA Corporation\Ansel\qUINT_ptgi_specular.fx");
    Console.WriteLine("Moved specular");
    Directory.Move(root + @"PTGI\Shaders\RTGI", @"C:\Program Files\NVIDIA Corporation\Ansel\RTGI");
    Console.WriteLine("Moved RTGI (ptgi) folder");
    File.Move(root + @"PTGI\Shaders\qUINT_ptgi_common.fxh", @"C:\Program Files\NVIDIA Corporation\Ansel\qUINT_ptgi_common.fxh");
    Console.WriteLine("Moved qUINT common");


    Directory.Delete(root + "PTGI", true);


    Console.WriteLine("\n\n");
    Console.WriteLine("Successfully installed qUINT_rtgi and qUINT_ptgi.");
}

Console.WriteLine("Press Enter to close this. ");
while (Console.ReadKey().Key != ConsoleKey.Enter) { }