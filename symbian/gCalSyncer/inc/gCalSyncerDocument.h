/*
 ============================================================================
 Name		: gCalSyncerDocument.h
 Author	  : toolkits.googlecode.com
 Copyright   : New BSD License
 Description : Declares document class for application.
 ============================================================================
 */

#ifndef __GCALSYNCERDOCUMENT_h__
#define __GCALSYNCERDOCUMENT_h__

// INCLUDES
#include <akndoc.h>

// FORWARD DECLARATIONS
class CgCalSyncerAppUi;
class CEikApplication;

// CLASS DECLARATION

/**
 * CgCalSyncerDocument application class.
 * An instance of class CgCalSyncerDocument is the Document part of the
 * AVKON application framework for the gCalSyncer example application.
 */
class CgCalSyncerDocument : public CAknDocument
	{
public:
	// Constructors and destructor

	/**
	 * NewL.
	 * Two-phased constructor.
	 * Construct a CgCalSyncerDocument for the AVKON application aApp
	 * using two phase construction, and return a pointer
	 * to the created object.
	 * @param aApp Application creating this document.
	 * @return A pointer to the created instance of CgCalSyncerDocument.
	 */
	static CgCalSyncerDocument* NewL(CEikApplication& aApp);

	/**
	 * NewLC.
	 * Two-phased constructor.
	 * Construct a CgCalSyncerDocument for the AVKON application aApp
	 * using two phase construction, and return a pointer
	 * to the created object.
	 * @param aApp Application creating this document.
	 * @return A pointer to the created instance of CgCalSyncerDocument.
	 */
	static CgCalSyncerDocument* NewLC(CEikApplication& aApp);

	/**
	 * ~CgCalSyncerDocument
	 * Virtual Destructor.
	 */
	virtual ~CgCalSyncerDocument();

public:
	// Functions from base classes

	/**
	 * CreateAppUiL
	 * From CEikDocument, CreateAppUiL.
	 * Create a CgCalSyncerAppUi object and return a pointer to it.
	 * The object returned is owned by the Uikon framework.
	 * @return Pointer to created instance of AppUi.
	 */
	CEikAppUi* CreateAppUiL();

private:
	// Constructors

	/**
	 * ConstructL
	 * 2nd phase constructor.
	 */
	void ConstructL();

	/**
	 * CgCalSyncerDocument.
	 * C++ default constructor.
	 * @param aApp Application creating this document.
	 */
	CgCalSyncerDocument(CEikApplication& aApp);

	};

#endif // __GCALSYNCERDOCUMENT_h__
// End of File
