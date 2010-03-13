/*
 ============================================================================
 Name		: DateDiffApplication.h
 Author	  : toolkits.googlecode.com
 Copyright   : New BSD License
 Description : Declares main application class.
 ============================================================================
 */

#ifndef __DATEDIFFAPPLICATION_H__
#define __DATEDIFFAPPLICATION_H__

// INCLUDES
#include <aknapp.h>
#include "DateDiff.hrh"

// UID for the application;
// this should correspond to the uid defined in the mmp file
const TUid KUidDateDiffApp =
	{
	_UID3
	};

// CLASS DECLARATION

/**
 * CDateDiffApplication application class.
 * Provides factory to create concrete document object.
 * An instance of CDateDiffApplication is the application part of the
 * AVKON application framework for the DateDiff example application.
 */
class CDateDiffApplication : public CAknApplication
	{
public:
	// Functions from base classes

	/**
	 * From CApaApplication, AppDllUid.
	 * @return Application's UID (KUidDateDiffApp).
	 */
	TUid AppDllUid() const;

protected:
	// Functions from base classes

	/**
	 * From CApaApplication, CreateDocumentL.
	 * Creates CDateDiffDocument document object. The returned
	 * pointer in not owned by the CDateDiffApplication object.
	 * @return A pointer to the created document object.
	 */
	CApaDocument* CreateDocumentL();
	};

#endif // __DATEDIFFAPPLICATION_H__
// End of File
