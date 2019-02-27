# CSharp Interface for Furhat #
This project holds an interface to interface with Furhat by using any .net environment. The project can be run as a C# console application as an example. However, its main purpose is to be compiled as a DLL project. 


## Testing the functionality of the Library
Check the [program.cs](TCPFurhatComm/Program.cs) file for a detailed example with the functionalities of this library. You can run this file as a console application by changing the project type in Visual Studio to a console application (under Project->Properties->Output type). Don't forget to change the IPaddress of the robot to the correct robot.

## Using the library as a dll
If you compile this project as a class library (under Project->Properties->Output type), it will generate a T[TCPFurhatComm.dll](TCPFurhatComm/bin/TCPFurhatComm.dll) file that can be used to control Furhat in Unity 3D or any .net environment. Don't forget to also copy [Newtonsoft.Json.dll](TCPFurhatComm/bin/Newtonsoft.Json.dll) to the folder where you are using the library as it requires it. You can also simply copy both files included in this gitrepository and start using the library.

## Additional Features
This interface is currently under development with some features missing for now. If you require new features or you want to discuss implementing additional features please contact me at atap@kth.se.