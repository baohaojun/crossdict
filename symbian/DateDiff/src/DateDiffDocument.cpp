/*
 ============================================================================
 Name		: DateDiffDocument.cpp
 Author	  : toolkits.googlecode.com
 Copyright   : New BSD License
 Description : CDateDiffDocument implementation
 ============================================================================
 */

// INCLUDE FILES
#include "DateDiffAppUi.h"
#include "DateDiffDocument.h"

// ============================ MEMBER FUNCTIONS ===============================

// -----------------------------------------------------------------------------
// CDateDiffDocument::NewL()
// Two-phased constructor.
// -----------------------------------------------------------------------------
//
CDateDiffDocument* CDateDiffDocument::NewL(CEikApplication& aApp)
	{
	CDateDiffDocument* self = NewLC(aApp);
	CleanupStack::Pop(self);
	return self;
	}

// -----------------------------------------------------------------------------
// CDateDiffDocument::NewLC()
// Two-phased constructor.
// -----------------------------------------------------------------------------
//
CDateDiffDocument* CDateDiffDocument::NewLC(CEikApplication& aApp)
	{
	CDateDiffDocument* self = new (ELeave) CDateDiffDocument(aApp);

	CleanupStack::PushL(self);
	self->ConstructL();
	return self;
	}

// -----------------------------------------------------------------------------
// CDateDiffDocument::ConstructL()
// Symbian 2nd phase constructor can leave.
// -----------------------------------------------------------------------------
//
void CDateDiffDocument::ConstructL()
	{
	// No implementation required
	}

// -----------------------------------------------------------------------------
// CDateDiffDocument::CDateDiffDocument()
// C++ default constructor can NOT contain any code, that might leave.
// -----------------------------------------------------------------------------
//
CDateDiffDocument::CDateDiffDocument(CEikApplication& aApp) :
	CAknDocument(aApp)
	{
	// No implementation required
	}

// ---------------------------------------------------------------------------
// CDateDiffDocument::~CDateDiffDocument()
// Destructor.
// ---------------------------------------------------------------------------
//
CDateDiffDocument::~CDateDiffDocument()
	{
	// No implementation required
	}

// ---------------------------------------------------------------------------
// CDateDiffDocument::CreateAppUiL()
// Constructs CreateAppUi.
// ---------------------------------------------------------------------------
//
CEikAppUi* CDateDiffDocument::CreateAppUiL()
	{
	// Create the application user interface, and return a pointer to it;
	// the framework takes ownership of this object
	return new (ELeave) CDateDiffAppUi;
	}

// End of File
