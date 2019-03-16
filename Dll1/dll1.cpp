#include "pch.h"
#include "dll1.h"

#include <iostream>
#include <functional>
#include <string>



extern "C" {
	__declspec(dllexport) int PassStructCase(FundamentalTypes fundamentalTypes, FundamentalTypes& fundamentalTypesRef, FundamentalTypes* fundamentalTypesPtr)
	{
		std::function<void(FundamentalTypes)> printFundamentalTypes = [](FundamentalTypes fundamentalTypes)
		{
			std::cout << "boolVal:" << fundamentalTypes.boolVal << std::endl;
			std::cout << "charVal:" << (int)fundamentalTypes.charVal << std::endl;
			std::cout << "ucharVal:" << (unsigned int)fundamentalTypes.ucharVal << std::endl;
			std::cout << "shortVal:" << fundamentalTypes.shortVal << std::endl;
			std::cout << "ushortVal:" << fundamentalTypes.ushortVal << std::endl;
			std::cout << "intVal:" << fundamentalTypes.intVal << std::endl;
			std::cout << "uintVal:" << fundamentalTypes.uintVal << std::endl;
			std::cout << "longlongVal:" << fundamentalTypes.longlongVal << std::endl;
			std::cout << "ulonglongVal:" << fundamentalTypes.ulonglongVal << std::endl;
			std::cout << "floatVal:" << fundamentalTypes.floatVal << std::endl;
			std::cout << "doubleVal:" << fundamentalTypes.doubleVal << std::endl;
		};
		std::cout << "\ncpp received fundamentalTypes:\n";
		printFundamentalTypes(fundamentalTypes);
		std::cout << "\ncpp received fundamentalTypesRef:\n";
		printFundamentalTypes(fundamentalTypesRef);
		std::cout << "\ncpp received fundamentalTypesPtr:\n";
		printFundamentalTypes(*fundamentalTypesPtr);
		std::function<void(FundamentalTypes&)> changeFundamentalTypes = [](FundamentalTypes & fundamentalTypes)
		{
			fundamentalTypes.boolVal = true;
			fundamentalTypes.charVal = -111;
			fundamentalTypes.ucharVal = 111;
			fundamentalTypes.shortVal = -11111;
			fundamentalTypes.ushortVal = 11111;
			fundamentalTypes.intVal = -1111111111;
			fundamentalTypes.uintVal = 1111111111;
			fundamentalTypes.longlongVal = -1111111111111111111;
			fundamentalTypes.ulonglongVal = 1111111111111111111;
			fundamentalTypes.floatVal = 111.1111f;
			fundamentalTypes.doubleVal = 111.111111111111;
		};
		changeFundamentalTypes(fundamentalTypes);
		changeFundamentalTypes(fundamentalTypesRef);
		changeFundamentalTypes(*fundamentalTypesPtr);
		std::cout << "\ncpp changed fundamentalTypes:\n";
		printFundamentalTypes(fundamentalTypes);
		std::cout << "\ncpp changed fundamentalTypesRef:\n";
		printFundamentalTypes(fundamentalTypesRef);
		std::cout << "\ncpp changed fundamentalTypesPtr:\n";
		printFundamentalTypes(*fundamentalTypesPtr);
		return 0;
	}

	__declspec(dllexport) int PassBufferCase(char* buffer)
	{
		std::cout << "cpp received buffer:" << buffer << std::endl;
		std::string("message from cpp").copy(buffer, 1024);
		std::cout << "cpp changed buffer:" << buffer << std::endl;
		return 0;
	}

	__declspec(dllexport) int PassBufferAndReallocMemoryCase(char* buffer, CallBackMemoryAlloc memAllocFunc)
	{
		std::cout << "cpp received buffer:" << buffer << std::endl;
		std::string message = "cpp call back to cs in order to alloc new bigger out buffer";
		char* newBuffer = nullptr;
		if (memAllocFunc(&newBuffer, message.length()))
		{
			std::cout << "cpp CallBackMemoryAlloc error\n";
			return -1;
		}
		message.copy(newBuffer, message.length());
		std::cout << "cpp alloc new buffer:";
		std::cout.write(newBuffer, message.length()) << std::endl;
		return 0;
	}
}