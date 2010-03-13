/*
 ============================================================================
 Name		: DateDiffAppUi.h
 Author	  : toolkits.googlecode.com
 Copyright   : New BSD License
 Description : Declares UI class for application.
 ============================================================================
 */

#ifndef __DATEDIFFAPPUI_h__
#define __DATEDIFFAPPUI_h__

// INCLUDES
#include <aknappui.h>

// FORWARD DECLARATIONS
class CDateDiffAppView;

// CLASS DECLARATION
/**
 * CDateDiffAppUi application UI class.
 * Interacts with the user through the UI and request message processing
 * from the handler class
 */
class CDateDiffAppUi : public CAknAppUi
	{
public:
	// Constructors and destructor

	/**
	 * ConstructL.
	 * 2nd phase constructor.
	 */
	void ConstructL();

	/**
	 * CDateDiffAppUi.
	 * C++ default constructor. This needs to be public due to
	 * the way the framework constructs the AppUi
	 */
	CDateDiffAppUi();

	/**
	 * ~CDateDiffAppUi.
	 * Virtual Destructor.
	 */
	virtual ~CDateDiffAppUi();

private:
	// Functions from base classes

	/**
	 * From CEikAppUi, HandleCommandL.
	 * Takes care of command handling.
	 * @param aCommand Command to be handled.
	 */
	void HandleCommandL(TInt aCommand);

	/**
	 *  HandleStatusPaneSizeChange.
	 *  Called by the framework when the application status pane
	 *  size is changed.
	 */
	void HandleStatusPaneSizeChange();

	/**
	 *  From CCoeAppUi, HelpContextL.
	 *  Provides help context for the application.
	 *  size is changed.
	 */
	CArrayFix<TCoeHelpContext>* HelpContextL() const;

private:
	// Data

	/**
	 * The application view
	 * Owned by CDateDiffAppUi
	 */
	CDateDiffAppView* iAppView;

	};

#endif // __DATEDIFFAPPUI_h__
// End of File
