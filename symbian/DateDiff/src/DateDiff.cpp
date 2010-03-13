/*
 ============================================================================
 Name		: DateDiff.cpp
 Author	  : toolkits.googlecode.com
 Copyright   : New BSD License
 Description : Main application class
 ============================================================================
 */

// INCLUDE FILES
#include <eikstart.h>
#include "DateDiffApplication.h"

LOCAL_C CApaApplication* NewApplication()
	{
	return new CDateDiffApplication;
	}

GLDEF_C TInt E32Main()
	{
	return EikStart::RunApplication(NewApplication);
	}

