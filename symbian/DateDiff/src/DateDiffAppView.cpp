/*
 ============================================================================
 Name		: DateDiffAppView.cpp
 Author	  : toolkits.googlecode.com
 Copyright   : New BSD License
 Description : Application view implementation
 ============================================================================
 */

// INCLUDE FILES
#include <coemain.h>
#include <eikenv.h>
#include <UTF.H>
#include "DateDiffAppView.h"

// ============================ MEMBER FUNCTIONS ===============================

// -----------------------------------------------------------------------------
// CDateDiffAppView::NewL()
// Two-phased constructor.
// -----------------------------------------------------------------------------
//
CDateDiffAppView* CDateDiffAppView::NewL(const TRect& aRect)
	{
	CDateDiffAppView* self = CDateDiffAppView::NewLC(aRect);
	CleanupStack::Pop(self);
	return self;
	}

// -----------------------------------------------------------------------------
// CDateDiffAppView::NewLC()
// Two-phased constructor.
// -----------------------------------------------------------------------------
//
CDateDiffAppView* CDateDiffAppView::NewLC(const TRect& aRect)
	{
	CDateDiffAppView* self = new (ELeave) CDateDiffAppView;
	CleanupStack::PushL(self);
	self->ConstructL(aRect);
	return self;
	}

// -----------------------------------------------------------------------------
// CDateDiffAppView::ConstructL()
// Symbian 2nd phase constructor can leave.
// -----------------------------------------------------------------------------
//
void CDateDiffAppView::ConstructL(const TRect& aRect)
	{
	// Create a window for this application view
	CreateWindowL();

	// Set the windows size
	SetRect(aRect);

	// Activate the window, which makes it ready to be drawn
	ActivateL();
	}

// -----------------------------------------------------------------------------
// CDateDiffAppView::CDateDiffAppView()
// C++ default constructor can NOT contain any code, that might leave.
// -----------------------------------------------------------------------------
//
CDateDiffAppView::CDateDiffAppView()
	{
	// No implementation required
	}

// -----------------------------------------------------------------------------
// CDateDiffAppView::~CDateDiffAppView()
// Destructor.
// -----------------------------------------------------------------------------
//
CDateDiffAppView::~CDateDiffAppView()
	{
	// No implementation required
	}

// -----------------------------------------------------------------------------
// CDateDiffAppView::Draw()
// Draws the display.
// -----------------------------------------------------------------------------
//
void CDateDiffAppView::Draw(const TRect& /*aRect*/) const
{
	// Get the standard graphics context
	CWindowGc& gc = SystemGc();

	// Gets the control's extent
	TRect drawRect(Rect());

	// Clears the screen
	gc.Clear(drawRect);
	
	//*
	_LIT8(KWelcome, "计算日期差");
	TBufC8<16> str8(KWelcome);
	TBuf<64> WelMsg;
	CnvUtfConverter::ConvertToUnicodeFromUtf8(WelMsg, str8); 
	gc.UseFont(CEikonEnv::Static()->DenseFont());
	gc.DrawText(WelMsg, TPoint(10, 40));
	gc.DiscardFont();
	//*/
}

// -----------------------------------------------------------------------------
// CDateDiffAppView::SizeChanged()
// Called by framework when the view size is changed.
// -----------------------------------------------------------------------------
//
void CDateDiffAppView::SizeChanged()
	{
	DrawNow();
	}

// -----------------------------------------------------------------------------
// CDateDiffAppView::HandlePointerEventL()
// Called by framework to handle pointer touch events.
// Note: although this method is compatible with earlier SDKs, 
// it will not be called in SDKs without Touch support.
// -----------------------------------------------------------------------------
//
void CDateDiffAppView::HandlePointerEventL(const TPointerEvent& aPointerEvent)
	{

	// Call base class HandlePointerEventL()
	CCoeControl::HandlePointerEventL(aPointerEvent);
	}

// End of File
