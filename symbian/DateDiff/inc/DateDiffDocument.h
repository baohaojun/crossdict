/*
 ============================================================================
 Name		: DateDiffDocument.h
 Author	  : toolkits.googlecode.com
 Copyright   : New BSD License
 Description : Declares document class for application.
 ============================================================================
 */

#ifndef __DATEDIFFDOCUMENT_h__
#define __DATEDIFFDOCUMENT_h__

// INCLUDES
#include <akndoc.h>

// FORWARD DECLARATIONS
class CDateDiffAppUi;
class CEikApplication;

// CLASS DECLARATION

/**
 * CDateDiffDocument application class.
 * An instance of class CDateDiffDocument is the Document part of the
 * AVKON application framework for the DateDiff example application.
 */
class CDateDiffDocument : public CAknDocument
	{
public:
	// Constructors and destructor

	/**
	 * NewL.
	 * Two-phased constructor.
	 * Construct a CDateDiffDocument for the AVKON application aApp
	 * using two phase construction, and return a pointer
	 * to the created object.
	 * @param aApp Application creating this document.
	 * @return A pointer to the created instance of CDateDiffDocument.
	 */
	static CDateDiffDocument* NewL(CEikApplication& aApp);

	/**
	 * NewLC.
	 * Two-phased constructor.
	 * Construct a CDateDiffDocument for the AVKON application aApp
	 * using two phase construction, and return a pointer
	 * to the created object.
	 * @param aApp Application creating this document.
	 * @return A pointer to the created instance of CDateDiffDocument.
	 */
	static CDateDiffDocument* NewLC(CEikApplication& aApp);

	/**
	 * ~CDateDiffDocument
	 * Virtual Destructor.
	 */
	virtual ~CDateDiffDocument();

public:
	// Functions from base classes

	/**
	 * CreateAppUiL
	 * From CEikDocument, CreateAppUiL.
	 * Create a CDateDiffAppUi object and return a pointer to it.
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
	 * CDateDiffDocument.
	 * C++ default constructor.
	 * @param aApp Application creating this document.
	 */
	CDateDiffDocument(CEikApplication& aApp);

	};

#endif // __DATEDIFFDOCUMENT_h__
// End of File
