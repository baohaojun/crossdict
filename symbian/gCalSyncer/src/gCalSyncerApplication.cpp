/*
 ============================================================================
 Name		: gCalSyncerApplication.cpp
 Author	  : toolkits.googlecode.com
 Copyright   : New BSD License
 Description : Main application class
 ============================================================================
 */

// INCLUDE FILES
#include "gCalSyncer.hrh"
#include "gCalSyncerDocument.h"
#include "gCalSyncerApplication.h"

// ============================ MEMBER FUNCTIONS ===============================

// -----------------------------------------------------------------------------
// CgCalSyncerApplication::CreateDocumentL()
// Creates CApaDocument object
// -----------------------------------------------------------------------------
//
CApaDocument* CgCalSyncerApplication::CreateDocumentL()
	{
	// Create an gCalSyncer document, and return a pointer to it
	return CgCalSyncerDocument::NewL(*this);
	}

// -----------------------------------------------------------------------------
// CgCalSyncerApplication::AppDllUid()
// Returns application UID
// -----------------------------------------------------------------------------
//
TUid CgCalSyncerApplication::AppDllUid() const
	{
	// Return the UID for the gCalSyncer application
	return KUidgCalSyncerApp;
	}

// End of File
