; Script generated by the Inno Setup Script Wizard.
; SEE THE DOCUMENTATION FOR DETAILS ON CREATING INNO SETUP SCRIPT FILES!

#define MyAppPublisher "Dinther Product Design"
#define MyAppURL "https://github.com/dinther/SerialPortManager"
#define ApplicationVersion GetFileVersion('C:\Users\Paul\source\repos\SerialPortManager\SerialPortManagerDemo\bin\Release\net6.0-windows\SerialPortManagerDemo.exe')

[Setup]
; NOTE: The value of AppId uniquely identifies this application. Do not use the same AppId value in installers for other applications.
; (To generate a new GUID, click Tools | Generate GUID inside the IDE.)
AppId={{E369F218-4629-423E-A3F8-3B146AFB5021}
AppName={#ApplicationName}
AppVersion={#ApplicationVersion}
AppPublisher={#MyAppPublisher}
AppPublisherURL={#MyAppURL}
AppSupportURL={#MyAppURL}
AppUpdatesURL={#MyAppURL}
DefaultDirName={autopf}\{#ApplicationName}
DisableProgramGroupPage=yes
; Uncomment the following line to run in non administrative install mode (install for current user only.)
;PrivilegesRequired=lowest
OutputDir=C:\Users\Paul\source\repos\SerialPortManager\SerialPortManagerDemo\bin\Publish
OutputBaseFilename=SerialPortManagerDemoSetup
Compression=lzma
SolidCompression=yes
WizardStyle=modern

[Languages]
Name: "english"; MessagesFile: "compiler:Default.isl"

[Tasks]
Name: "desktopicon"; Description: "{cm:CreateDesktopIcon}"; GroupDescription: "{cm:AdditionalIcons}"; Flags: unchecked

[Files]
Source: "C:\Users\Paul\source\repos\SerialPortManager\SerialPortManagerDemo\bin\Release\net6.0-windows\SerialPortManagerDemo.exe"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Paul\source\repos\SerialPortManager\SerialPortManagerDemo\bin\Release\net6.0-windows\SerialPortManager.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Paul\source\repos\SerialPortManager\SerialPortManagerDemo\bin\Release\net6.0-windows\SerialPortManagerDemo.deps.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Paul\source\repos\SerialPortManager\SerialPortManagerDemo\bin\Release\net6.0-windows\SerialPortManagerDemo.dll"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Paul\source\repos\SerialPortManager\SerialPortManagerDemo\bin\Release\net6.0-windows\SerialPortManagerDemo.runtimeconfig.json"; DestDir: "{app}"; Flags: ignoreversion
Source: "C:\Users\Paul\source\repos\SerialPortManager\SerialPortManagerDemo\bin\Release\net6.0-windows\System.Management.dll"; DestDir: "{app}"; Flags: ignoreversion
; NOTE: Don't use "Flags: ignoreversion" on any shared system files

[Icons]
Name: "{autoprograms}\AppName"; Filename: "{app}\{#ApplicationName}.exe"
Name: "{autodesktop}\AppName"; Filename: "{app}\{#ApplicationName}.exe"; Tasks: desktopicon

[Run]
Filename: "{app}\{#ApplicationName}.exe"; Description: "{cm:LaunchProgram,{#ApplicationName}}"; Flags: nowait postinstall skipifsilent