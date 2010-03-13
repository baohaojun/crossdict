/*
 ============================================================================
 Name		: DateDiffApplication.cpp
 Author	  : toolkits.googlecode.com
 Copyright   : New BSD License
 Description : Main application class
 ============================================================================
 */

// INCLUDE FILES
#include "DateDiff.hrh"
#include "DateDiffDocument.h"
#include "DateDiffApplication.h"

// ============================ MEMBER FUNCTIONS ===============================

// -----------------------------------------------------------------------------
// CDateDiffApplication::CreateDocumentL()
// Creates CApaDocument object
// -----------------------------------------------------------------------------
//
CApaDocument* CDateDiffApplication::CreateDocumentL()
	{
	// Create an DateDiff document, and return a pointer to it
	return CDateDiffDocument::NewL(*this);
	}

// -----------------------------------------------------------------------------
// CDateDiffApplication::AppDllUid()
// Returns application UID
// -----------------------------------------------------------------------------
//
TUid CDateDiffApplication::AppDllUid() const
	{
	// Return the UID for the DateDiff application
	return KUidDateDiffApp;
	}

// End of File
