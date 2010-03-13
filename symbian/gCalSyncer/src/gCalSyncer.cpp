/*
 ============================================================================
 Name		: gCalSyncer.cpp
 Author	  : toolkits.googlecode.com
 Copyright   : New BSD License
 Description : Main application class
 ============================================================================
 */

// INCLUDE FILES
#include <eikstart.h>
#include "gCalSyncerApplication.h"

LOCAL_C CApaApplication* NewApplication()
	{
	return new CgCalSyncerApplication;
	}

GLDEF_C TInt E32Main()
	{
	return EikStart::RunApplication(NewApplication);
	}

