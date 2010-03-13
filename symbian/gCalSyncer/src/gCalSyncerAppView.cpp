/*
 ============================================================================
 Name		: gCalSyncerAppView.cpp
 Author	  : toolkits.googlecode.com
 Copyright   : New BSD License
 Description : Application view implementation
 ============================================================================
 */

// INCLUDE FILES
#include <coemain.h>
#include <stringloader.h>
#include "gCalSyncerAppView.h"

#include <gCalSyncer_0xE9062F6D.rsg>
// ============================ MEMBER FUNCTIONS ===============================

// -----------------------------------------------------------------------------
// CgCalSyncerAppView::NewL()
// Two-phased constructor.
// -----------------------------------------------------------------------------
//
CgCalSyncerAppView* CgCalSyncerAppView::NewL(const TRect& aRect)
	{
	CgCalSyncerAppView* self = CgCalSyncerAppView::NewLC(aRect);
	CleanupStack::Pop(self);
	return self;
	}

// -----------------------------------------------------------------------------
// CgCalSyncerAppView::NewLC()
// Two-phased constructor.
// -----------------------------------------------------------------------------
//
CgCalSyncerAppView* CgCalSyncerAppView::NewLC(const TRect& aRect)
	{
	CgCalSyncerAppView* self = new (ELeave) CgCalSyncerAppView;
	CleanupStack::PushL(self);
	self->ConstructL(aRect);
	return self;
	}

// -----------------------------------------------------------------------------
// CgCalSyncerAppView::ConstructL()
// Symbian 2nd phase constructor can leave.
// -----------------------------------------------------------------------------
//
void CgCalSyncerAppView::ConstructL(const TRect& aRect)
	{
	// Create a window for this application view
	CreateWindowL();

	// Set the windows size
	SetRect(aRect);

	// Activate the window, which makes it ready to be drawn
	ActivateL();
	}

// -----------------------------------------------------------------------------
// CgCalSyncerAppView::CgCalSyncerAppView()
// C++ default constructor can NOT contain any code, that might leave.
// -----------------------------------------------------------------------------
//
CgCalSyncerAppView::CgCalSyncerAppView()
	{
	// No implementation required
	}

// -----------------------------------------------------------------------------
// CgCalSyncerAppView::~CgCalSyncerAppView()
// Destructor.
// -----------------------------------------------------------------------------
//
CgCalSyncerAppView::~CgCalSyncerAppView()
	{
	// No implementation required
	}

// -----------------------------------------------------------------------------
// CgCalSyncerAppView::Draw()
// Draws the display.
// -----------------------------------------------------------------------------
//
void CgCalSyncerAppView::Draw(const TRect& aRect) const
	{
	// Get the standard graphics context
	CWindowGc& gc = SystemGc();

	// Gets the control's extent
	TRect drawRect(Rect());

	// Clears the screen
	gc.Clear(drawRect);
	
	// Load a string from the resource file and display it
	HBufC* textResource = StringLoader::LoadLC(R_WELCOME_TEXT);
	TBuf<64> WelMsg(*textResource);
	TPoint namePoint(aRect.Width()/5*3, aRect.Height()/4); 
	gc.DrawText(WelMsg, TPoint(10, 40));
	CleanupStack::PopAndDestroy(textResource);
	}

// -----------------------------------------------------------------------------
// CgCalSyncerAppView::SizeChanged()
// Called by framework when the view size is changed.
// -----------------------------------------------------------------------------
//
void CgCalSyncerAppView::SizeChanged()
	{
	DrawNow();
	}

// -----------------------------------------------------------------------------
// CgCalSyncerAppView::HandlePointerEventL()
// Called by framework to handle pointer touch events.
// Note: although this method is compatible with earlier SDKs, 
// it will not be called in SDKs without Touch support.
// -----------------------------------------------------------------------------
//
void CgCalSyncerAppView::HandlePointerEventL(const TPointerEvent& aPointerEvent)
	{

	// Call base class HandlePointerEventL()
	CCoeControl::HandlePointerEventL(aPointerEvent);
	}

// End of File
