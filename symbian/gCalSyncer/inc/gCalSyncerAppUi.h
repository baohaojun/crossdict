/*
 ============================================================================
 Name		: gCalSyncerAppUi.h
 Author	  : toolkits.googlecode.com
 Copyright   : New BSD License
 Description : Declares UI class for application.
 ============================================================================
 */

#ifndef __GCALSYNCERAPPUI_h__
#define __GCALSYNCERAPPUI_h__

// INCLUDES
#include <aknappui.h>

// FORWARD DECLARATIONS
class CgCalSyncerAppView;

// CLASS DECLARATION
/**
 * CgCalSyncerAppUi application UI class.
 * Interacts with the user through the UI and request message processing
 * from the handler class
 */
class CgCalSyncerAppUi : public CAknAppUi
	{
public:
	// Constructors and destructor

	/**
	 * ConstructL.
	 * 2nd phase constructor.
	 */
	void ConstructL();

	/**
	 * CgCalSyncerAppUi.
	 * C++ default constructor. This needs to be public due to
	 * the way the framework constructs the AppUi
	 */
	CgCalSyncerAppUi();

	/**
	 * ~CgCalSyncerAppUi.
	 * Virtual Destructor.
	 */
	virtual ~CgCalSyncerAppUi();

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
	 * Owned by CgCalSyncerAppUi
	 */
	CgCalSyncerAppView* iAppView;

	};

#endif // __GCALSYNCERAPPUI_h__
// End of File
