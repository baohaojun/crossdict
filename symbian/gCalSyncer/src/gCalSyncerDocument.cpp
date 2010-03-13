/*
 ============================================================================
 Name		: gCalSyncerDocument.cpp
 Author	  : toolkits.googlecode.com
 Copyright   : New BSD License
 Description : CgCalSyncerDocument implementation
 ============================================================================
 */

// INCLUDE FILES
#include "gCalSyncerAppUi.h"
#include "gCalSyncerDocument.h"

// ============================ MEMBER FUNCTIONS ===============================

// -----------------------------------------------------------------------------
// CgCalSyncerDocument::NewL()
// Two-phased constructor.
// -----------------------------------------------------------------------------
//
CgCalSyncerDocument* CgCalSyncerDocument::NewL(CEikApplication& aApp)
	{
	CgCalSyncerDocument* self = NewLC(aApp);
	CleanupStack::Pop(self);
	return self;
	}

// -----------------------------------------------------------------------------
// CgCalSyncerDocument::NewLC()
// Two-phased constructor.
// -----------------------------------------------------------------------------
//
CgCalSyncerDocument* CgCalSyncerDocument::NewLC(CEikApplication& aApp)
	{
	CgCalSyncerDocument* self = new (ELeave) CgCalSyncerDocument(aApp);

	CleanupStack::PushL(self);
	self->ConstructL();
	return self;
	}

// -----------------------------------------------------------------------------
// CgCalSyncerDocument::ConstructL()
// Symbian 2nd phase constructor can leave.
// -----------------------------------------------------------------------------
//
void CgCalSyncerDocument::ConstructL()
	{
	// No implementation required
	}

// -----------------------------------------------------------------------------
// CgCalSyncerDocument::CgCalSyncerDocument()
// C++ default constructor can NOT contain any code, that might leave.
// -----------------------------------------------------------------------------
//
CgCalSyncerDocument::CgCalSyncerDocument(CEikApplication& aApp) :
	CAknDocument(aApp)
	{
	// No implementation required
	}

// ---------------------------------------------------------------------------
// CgCalSyncerDocument::~CgCalSyncerDocument()
// Destructor.
// ---------------------------------------------------------------------------
//
CgCalSyncerDocument::~CgCalSyncerDocument()
	{
	// No implementation required
	}

// ---------------------------------------------------------------------------
// CgCalSyncerDocument::CreateAppUiL()
// Constructs CreateAppUi.
// ---------------------------------------------------------------------------
//
CEikAppUi* CgCalSyncerDocument::CreateAppUiL()
	{
	// Create the application user interface, and return a pointer to it;
	// the framework takes ownership of this object
	return new (ELeave) CgCalSyncerAppUi;
	}

// End of File
