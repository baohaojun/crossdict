/*
 ============================================================================
 Name		: gCalSyncerApplication.h
 Author	  : toolkits.googlecode.com
 Copyright   : New BSD License
 Description : Declares main application class.
 ============================================================================
 */

#ifndef __GCALSYNCERAPPLICATION_H__
#define __GCALSYNCERAPPLICATION_H__

// INCLUDES
#include <aknapp.h>
#include "gCalSyncer.hrh"

// UID for the application;
// this should correspond to the uid defined in the mmp file
const TUid KUidgCalSyncerApp =
	{
	_UID3
	};

// CLASS DECLARATION

/**
 * CgCalSyncerApplication application class.
 * Provides factory to create concrete document object.
 * An instance of CgCalSyncerApplication is the application part of the
 * AVKON application framework for the gCalSyncer example application.
 */
class CgCalSyncerApplication : public CAknApplication
	{
public:
	// Functions from base classes

	/**
	 * From CApaApplication, AppDllUid.
	 * @return Application's UID (KUidgCalSyncerApp).
	 */
	TUid AppDllUid() const;

protected:
	// Functions from base classes

	/**
	 * From CApaApplication, CreateDocumentL.
	 * Creates CgCalSyncerDocument document object. The returned
	 * pointer in not owned by the CgCalSyncerApplication object.
	 * @return A pointer to the created document object.
	 */
	CApaDocument* CreateDocumentL();
	};

#endif // __GCALSYNCERAPPLICATION_H__
// End of File
