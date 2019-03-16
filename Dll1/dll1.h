#pragma once




extern "C" {

	struct FundamentalTypes
	{
		bool boolVal;
		unsigned char ucharVal;
		char charVal;
		short shortVal;
		unsigned short ushortVal;
		int intVal;
		unsigned int uintVal;
		long long longlongVal;
		unsigned long long ulonglongVal;
		float floatVal;
		double doubleVal;
	};

	typedef int CallBackMemoryAlloc(char**, int);

	__declspec(dllexport) int PassStructCase(FundamentalTypes fundamentalTypes, FundamentalTypes& fundamentalTypesRef, FundamentalTypes* fundamentalTypesPtr);
	__declspec(dllexport) int PassBufferCase(char* buffer);
	__declspec(dllexport) int PassBufferAndReallocMemoryCase(char* buffer, CallBackMemoryAlloc memAllocFunc);
}