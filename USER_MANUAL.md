# Sphere ERP — User Manual

**Version 1.1**  
Egyptian Tax Authority (ETA) Electronic Invoicing — Sphere ERP

---

## Table of Contents

1. [Introduction](#1-introduction)
2. [Login & Getting Started](#2-login--getting-started)
3. [Main Dashboard](#3-main-dashboard)
4. [Creating Documents](#4-creating-documents)
5. [Line Item Editor](#5-line-item-editor)
6. [EGS Code Library](#6-egs-code-library)
7. [Batch Import (CSV / Excel)](#7-batch-import-csv--excel)
8. [CSV / Excel Import Format](#8-csv--excel-import-format)
9. [Search & Manage Documents](#9-search--manage-documents)
10. [Notifications](#10-notifications)
11. [Settings](#11-settings)
12. [Validation Rules](#12-validation-rules)
13. [Keyboard Shortcuts](#13-keyboard-shortcuts)
14. [Troubleshooting & FAQ](#14-troubleshooting--faq)

---

## 1. Introduction

**Sphere ERP** is a Windows application for creating, validating, digitally signing, submitting, and managing electronic invoices required by the Egyptian Tax Authority (ETA). It connects directly to the ETA's official API gateways for both pre-production testing and live production.

### Key Capabilities

- Create Invoices, Credit Notes, Debit Notes, and Export variants (EI / EC / ED)
- Digitally sign documents using a USB token / HSM certificate (CADES-BES signature)
- Submit documents to the ETA portal
- Search submitted documents, download PDFs, cancel or reject documents
- Import documents in bulk from CSV or Excel files
- Manage EGS (Egyptian Government Standard) item codes
- Receive desktop and email notifications on document status changes
- Support both **Pre-production** (testing) and **Production** (live) environments

### Supported Document Types

| Code | Type | Description |
|---|---|---|
| **i** | Invoice | Standard domestic tax invoice |
| **c** | Credit Note | Credit note referencing a previous invoice |
| **d** | Debit Note | Debit note referencing a previous invoice |
| **ei** | Export Invoice | Invoice for non-Egyptian buyers (export) |
| **ec** | Export Credit Note | Credit note referencing an export invoice |
| **ed** | Export Debit Note | Debit note referencing an export invoice |

### Technology

Built on .NET 8. Data stored in SQL Server or LocalDB.

### Prerequisites

- .NET 8 runtime
- SQL Server or SQL Server Express LocalDB
- A USB token / HSM containing a valid signing certificate (CADES-BES)
- ETA portal credentials (Client ID, Client Secret, Taxpayer RIN)

---

## 2. Login & Getting Started

### First Launch

When you start the application, the **Login** form appears.

### Login Fields

| Field | Description |
|---|---|
| **Client ID** | Your ETA portal application client ID (provided by ETA when you register your integration) |
| **Client Secret** | Your ETA portal application secret key (masked as dots while typing) |
| **Taxpayer RIN** | Your 9-digit Taxpayer Registration Identification Number |
| **Environment** | Choose **Preproduction** for testing or **Production** for live submissions |
| **Save Credentials** | Check to persist your credentials for next launch (encrypted locally via DPAPI) |

### How to Log In

1. Enter your **Client ID** and **Client Secret** from the ETA developer portal
2. Enter your 9-digit **Taxpayer RIN**
3. Select **Preproduction** (testing) or **Production** (live)
4. Optionally check **Save Credentials**
5. Click **Login**
6. If using a USB token for the first time, you may be prompted for a PIN during subsequent operations

### What Happens After Login

The application authenticates with ETA's OAuth2 token endpoint, initializes your local database, and opens the **Main Dashboard**. Your RIN and environment appear in the status bar at the bottom of the screen.

---

## 3. Main Dashboard

### Navigation Sidebar

Click any button on the left sidebar to navigate:

| Button | Description |
|---|---|
| **Dashboard** | KPI overview and recent documents |
| **New Document** | Create any document type (choose type in the entry form) |
| **Batch Import** | Import multiple documents from CSV or Excel |
| **Search** | Search and manage submitted documents |
| **EGS Codes** | Manage your item code library |
| **Notifications** | View status change notifications |
| **Settings** | Configure credentials, certificates, and preferences |
| **Sign Out** | Return to the login screen |

### Status Bar

The bottom bar shows your connected environment (Preproduction / Production) and your RIN.

### KPI Cards

Six colored cards display summary statistics for the current filter range:

| Card | Color | What It Shows |
|---|---|---|
| **Total Documents** | Blue | Count of all documents matching the current filter |
| **Valid** | Green | Documents that passed validation |
| **Submitted / Pending** | Amber | Documents submitted to ETA awaiting processing |
| **Rejected / Invalid** | Red | Documents that failed ETA validation |
| **Cancelled** | Gray | Cancelled documents |
| **Total Amount** | Purple | Sum of document totals for the current filter |

### Dashboard Filters

Filter the dashboard data using the controls at the top-right:

- **From / To** dates — filter documents by issue date range
- **Direction** — drop-down to show All, Sent (issued by you), or Received (issued to you)

### Recent Documents Grid

Below the KPIs, a grid shows the most recent documents with columns: Internal ID, Direction (Sent/Received), Type, Receiver, Issue Date, Total, and Status. The grid footer displays a sum total.

---

## 4. Creating Documents

The **Invoice Entry** form is used for all document types. Click **New Document** in the sidebar to open it. Select the document type from the **Document Type** drop-down at the top-right of the form. The form title, header color, and reference UUID requirement adjust automatically based on the selected type.

### Currency Section

| Field | Description |
|---|---|
| **Currency** | Drop-down of 24 ISO 4217 currencies. Default: EGP (Egyptian Pound) |
| **Exchange Rate** | Shown only when a non-EGP currency is selected. Enter the rate from the foreign currency to 1 EGP |

The selected currency applies to all line items in the document.

### Document Details Section

| Field | Required | Description |
|---|---|---|
| **Internal ID** | Yes | Your internal invoice / reference number |
| **Issue Date/Time** | Yes | Date and time the document was issued. Auto-set to current date/time. Format: `yyyy-MM-dd HH:mm` |
| **Payment Method** | Yes | C=Cash, V=Visa/Card, M=Mobile Wallet, O=Other, B=Bank Transfer |
| **Reference Invoice UUID** | For notes | Required for Credit/Debit Notes (including export variants) — the UUID of the original invoice being credited or debited |

### Receiver (Buyer) Section

| Field | Required | Description |
|---|---|---|
| **Type** | Yes | B=Business, P=Natural Person, F=Foreigner |
| **RIN / National ID** | Yes | For Business (B): 9-digit RIN. For Person (P): National ID. For Foreigner (F): foreign ID |
| **Name** | Yes | Legal name of the receiver |
| **Country** | No | 44-country list with Arabic descriptions. Default: EG - مصر |
| **Governate** | No | Governorate / region |
| **Region/City** | No | City name |
| **Street** | No | Street address |
| **Bldg #** | No | Building number |

**Tip:** Double-click the RIN field to open the **Receiver Selector** dialog. Choose a saved receiver to auto-fill all receiver fields including country.

### Line Items Grid

Displays all line items added to the document. Each line shows Description, Item Code, Unit, Qty, Unit Price, Discount, Net, Tax, and Total. The grid footer shows subtotals for quantity, discount, net, tax, and total.

### Action Buttons

| Button | Description |
|---|---|
| **Add from EGS Library** | Opens the EGS Code Library to search and pick items |
| **Add** | Opens the Line Item Editor to add a new item |
| **Edit** | Opens the Line Item Editor with the selected item's data |
| **Remove** | Removes the selected item (with confirmation) |
| **Validate** | Runs all validation rules on the document and shows any errors |
| **Sign & Submit** | Validates, signs with your digital certificate, and submits to ETA |
| **Print / PDF** | Generates a PDF preview of the document |

### Document Lifecycle (Submission Flow)

1. Select the document type from the drop-down
2. Choose currency (if non-EGP, enter the exchange rate)
3. Fill in document details and receiver information
4. Add line items (manually or from the EGS library)
5. Click **Validate** to check for errors
6. Fix any validation issues
7. Click **Sign & Submit** — the app will:
   - Prompt for your USB token PIN if needed
   - Digitally sign the document (CADES-BES signature)
   - Submit to the ETA portal
   - Store the result (UUID, status) locally

---

## 5. Line Item Editor

Opened when adding or editing a single line item.

| Field | Required | Description |
|---|---|---|
| **Description** | Yes | Item description |
| **Item Code (EGS / GS1 / GPC)** | Yes | The item's standard code |
| **Item Type** | Yes | EGS, GS1, or GPC |
| **Unit Type** | Yes | Unit of measure (e.g., EA = each, KGM = kilogram, M = meter, L = litre) |
| **Quantity** | Yes | Quantity (must be > 0, default 1) |
| **Unit Price (EGP)** | Yes | Price per unit in EGP |
| **Discount Amount** | No | Discount amount for this line (default 0) |
| **Tax Type** | Yes | T1=VAT, T2=Table Tax, T3=Stamp Tax, up to T20 |
| **Tax Sub-Type** | Yes | Auto-populated based on selected Tax Type (e.g. V009 for T1) |
| **Tax Rate (%)** | Yes | Tax percentage (default 14% for T1) |
| **Line Total** | Display | Computed total shown at the bottom |

Click **OK** to save the line item or **Cancel** to discard.

---

## 6. EGS Code Library

The EGS Code Library lets you maintain your own item codes, or import codes already registered on the ETA portal.

### Grid Columns

EGS Code, Description, Unit Type, Default Price, Tax Type, Tax Rate, Category, Active.

### Actions

| Action | Description |
|---|---|
| **Search** | Type a code or description and click Search |
| **Refresh** | Reload all codes from the local database |
| **Import Codes from Portal** | Fetches your registered item codes from the ETA portal and merges them into your local library |
| **Select a row** | Click any row to load its details into the editor panel on the right |
| **New / Clear** | Clears the editor to create a new code |
| **Save** | Saves the code (inserts new or updates existing) |
| **Delete** | Deletes the selected code (with confirmation) |

### Editor Fields

EGS Code, Description, Unit Type, Default Price (EGP), Tax Type (T1–T10), Tax Rate (%), Tax Sub-Type, Category, Active (checkbox).

---

## 7. Batch Import (CSV / Excel)

Import multiple documents at once from a CSV or Excel file.

### How to Use

1. Click **Download Template** to get a sample CSV file with the correct column headers
2. Prepare your data following the template format (see [section 8](#8-csv--excel-import-format))
3. Click **Choose File** and select your CSV or Excel file
4. The preview grid shows each document with validation status — valid and invalid rows are color-coded
5. Double-click a row to see its full line-item details in a popup dialog
6. Click **Submit All Valid** to submit all valid documents to ETA
7. Progress is shown on the progress bar

### Data Structure

The import file uses a **flat-row layout**: each row represents one line item of an invoice. Consecutive rows with the same `InternalId` are grouped into a single document. The 26 columns fall into two categories:

| Category | Columns | Maps to ETA Structure |
|---|---|---|
| **Header fields** | 1–13 (`InternalId` through `ReferenceUuid`) | Core document — `issuer`, `receiver`, `payment`, `documentType`, `dateTimeIssued` |
| **Line item fields** | 14–26 (`ItemDescription` through `CurrencyExchangeRate`) | `invoiceLines[]` — each row becomes an `InvoiceLine` with its `Value`, `Discount`, and `TaxableItem` sub-structures |

**Grouping rule:** All rows sharing the same `InternalId` produce one document with multiple line items. Header values on the last row for a given `InternalId` (e.g., `ReceiverName`, `DateTimeIssued`) override earlier ones, so keep identical header data across grouped rows.

### Computed & Derived Amounts

The following amounts are **calculated by the application** and do not appear in the import file:

| Field | Formula |
|---|---|
| **Sales Total** | `Quantity × UnitPrice` (per line) |
| **Net Total** | `SalesTotal − DiscountAmount` (per line) |
| **Tax Amount** | `NetTotal × (TaxRate ÷ 100)` (per line) |
| **Total** | `NetTotal + TaxAmount` (per line) |
| **Document Total** | Sum of all line Totals for the document |

### Preview Grid Columns

Row, Internal ID, Type, Receiver, Issuer, Total, Status, Errors.

### Detail Dialog

When you double-click a row, a popup shows all fields including Description, Item Code, Item Type, Unit Type, Quantity, Unit Price, Discount, Tax Type, Tax Sub-Type, Tax Rate, Currency, Amount Sold, Exchange Rate, Sales Total, Net Total, Tax Amount, and Total.

---

## 8. CSV / Excel Import Format

The import file must have the following 26 columns (case-insensitive headers). Click **Download Template** on the Batch Import form to get a sample file.

| # | Column Name | Required | Description |
|---|---|---|---|
| 1 | `InternalId` | Yes | Internal document reference. Used as the **grouping key**: consecutive rows with the same InternalId are treated as line items of a single document. |
| 2 | `DocumentType` | Yes | Document type code: I = Invoice, C = Credit Note, D = Debit Note, EI = Export Invoice, EC = Export Credit Note, ED = Export Debit Note. |
| 3 | `DateTimeIssued` | Yes | Date and time of issue in UTC. Accepted formats: `yyyy-MM-ddTHH:mm:ssZ` (ISO 8601) or `yyyy-MM-dd HH:mm`. |
| 4 | `ReceiverType` | Yes | Receiver type: B = Business in Egypt, P = Natural person, F = Foreigner. |
| 5 | `ReceiverId` | Yes | Receiver identification: For B = 9-digit RIN, for P = National ID, for F = foreign VAT ID. |
| 6 | `ReceiverName` | Yes | Legal name of the receiver company or individual. |
| 7 | `ReceiverGovernate` | No | Receiver's governorate / region. Maps to ETA `receiver/address/governate`. |
| 8 | `ReceiverRegionCity` | No | Receiver's city or district. Maps to ETA `receiver/address/regionCity`. |
| 9 | `ReceiverStreet` | No | Receiver's street address. Maps to ETA `receiver/address/street`. |
| 10 | `ReceiverBuildingNumber` | No | Receiver's building number. Maps to ETA `receiver/address/buildingNumber`. |
| 11 | `ReceiverCountry` | No | Two-letter ISO-3166 country code. Default: EG. Maps to ETA `receiver/address/country`. |
| 12 | `PaymentMethod` | No | Payment method: C = Cash, V = Visa/Card, M = Mobile Wallet, O = Other, B = Bank Transfer. Default: C. |
| 13 | `ReferenceUuid` | No | Required for Credit Notes (C), Debit Notes (D), and export variants (EC, ED). The ETA UUID of the original invoice being credited or debited. |
| 14 | `ItemDescription` | Yes | Description of the goods or services sold. Maps to ETA `invoiceLine/description`. |
| 15 | `ItemCode` | Yes | Standard item code: EGS, GS1, or GPC code. Maps to ETA `invoiceLine/itemCode`. |
| 16 | `ItemType` | No | Coding schema: GS1, EGS, or GPC. Default: EGS. Maps to ETA `invoiceLine/itemType`. |
| 17 | `UnitType` | No | Unit of measure code (e.g., EA = each, KGM = kilogram, M = meter, L = litre). See ETA [unit types code table](https://sdk.invoicing.eta.gov.eg/codes/unit-types/). Default: EA. Maps to ETA `invoiceLine/unitType`. |
| 18 | `Quantity` | No | Number of units sold. Must be > 0. Default: 1. Maps to ETA `invoiceLine/quantity`. |
| 19 | `UnitPrice` | Yes | Price per single unit. For EGP transactions: value in EGP (maps to `invoiceLine/unitValue/amountEGP`). For foreign currency: value in the foreign currency (maps to `invoiceLine/unitValue/amountSold`). |
| 20 | `DiscountAmount` | No | Line-level discount amount in EGP. Default: 0. Maps to ETA `invoiceLine/discount/amount`. |
| 21 | `TaxType` | No | Tax type code: T1 = VAT, T2 = Table Tax, T3 = Stamp Tax, up to T20. Default: T1. Maps to ETA `invoiceLine/taxableItems[]/taxType`. |
| 22 | `TaxSubType` | No | Tax sub-type code (e.g., V009 for standard VAT rate). Auto-resolved from TaxType if left empty. Maps to ETA `invoiceLine/taxableItems[]/subType`. |
| 23 | `TaxRate` | No | Tax percentage rate (0–100). Default: 14 (14% VAT for T1). Maps to ETA `invoiceLine/taxableItems[]/rate`. |
| 24 | `CurrencySold` | No | ISO 4217 currency code (e.g., EGP, USD, EUR, SAR). Default: EGP. Maps to ETA `invoiceLine/unitValue/currencySold`. |
| 25 | `AmountSold` | No | Unit price expressed in the foreign currency. Required when `CurrencySold` is not EGP. Maps to ETA `invoiceLine/unitValue/amountSold`. |
| 26 | `CurrencyExchangeRate` | No | Exchange rate from the foreign currency to 1 EGP, using the Egyptian bank rate on the invoice date. Required when `CurrencySold` is not EGP. Maps to ETA `invoiceLine/unitValue/currencyExchangeRate`. |

### Grouping

Multiple line items for one document should use consecutive rows with identical header fields (columns 1–13). The importer groups them by InternalId. If header values differ across rows with the same InternalId, the values from the last row are used.

### Computed Amounts Reference

The import parser derives these amounts automatically — they do **not** appear as columns in the file:

| Derived Field | Formula | Maps to ETA |
|---|---|---|
| `SalesTotal` | `Quantity × UnitPrice` | `invoiceLine/salesTotal` |
| `NetTotal` | `SalesTotal − DiscountAmount` | `invoiceLine/netTotal` |
| `TaxAmount` | `NetTotal × (TaxRate ÷ 100)` | Sum of `invoiceLine/taxableItems[].amount` |
| `Total` (per line) | `NetTotal + TaxAmount` | `invoiceLine/total` |
| `totalSalesAmount` (document) | Sum of all line `SalesTotal` | `document/totalSalesAmount` |
| `netAmount` (document) | Sum of all line `NetTotal` | `document/netAmount` |
| `totalAmount` (document) | Sum of all line `Total` | `document/totalAmount` |

### ETA Sub-Structure Reference

The import columns populate these ETA Invoice Line sub-structures:

**Value** (`invoiceLine/unitValue`):

| Import Column(s) | ETA Field |
|---|---|
| `UnitPrice` (when `CurrencySold` = EGP) | `amountEGP` |
| `CurrencySold` | `currencySold` |
| `UnitPrice` / `AmountSold` (when non-EGP) | `amountSold` |
| `CurrencyExchangeRate` (when non-EGP) | `currencyExchangeRate` |

**Discount** (`invoiceLine/discount`):

| Import Column | ETA Field |
|---|---|
| `DiscountAmount` | `amount` |

**Taxable Item** (`invoiceLine/taxableItems[]`):

| Import Column | ETA Field |
|---|---|
| `TaxType` | `taxType` |
| `TaxSubType` | `subType` |
| `TaxRate` | `rate` |

---

## 9. Search & Manage Documents

The **Document Search** form lets you find and manage previously submitted documents.

### Filter Fields

| Filter | Type | Description |
|---|---|---|
| **Internal ID** | Text | Search by your internal reference number |
| **Direction** | Drop-down | All, Sent, or Received |
| **Receiver Name** | Text | Search by receiver name |
| **Issuer Name** | Text | Search by issuer name |
| **Type** | Drop-down | All, Invoice, Credit Note, Debit Note, Export Invoice, Export Credit Note, Export Debit Note |
| **Status** | Drop-down | All, Valid, Invalid, Submitted, Rejected, Cancelled, Failed |
| **From / To** | Date pickers | Date range filter |

Click **Search** to run the query.

### Results Grid

Columns: Internal ID, Direction, Type, Receiver, Issuer, Issue Date, Total (currency), Status, UUID.

### Detail Sub-Grid

When you select a document in the results grid, its line items appear in the bottom panel showing Description, Item Code, Unit, Qty, Unit Price, Net Total, Discount, Tax Amount.

### Action Buttons

| Button | Description |
|---|---|
| **Download PDF** | Downloads the ETA-stamped PDF for the selected document |
| **Cancel Document** | Cancels the selected document on the portal (with confirmation and reason) |
| **Reject Document** | Rejects the selected document (with confirmation and reason) |
| **Sync from Portal** | Fetches the latest document status from the ETA portal |
| **View Details** | Shows the raw document JSON in a popup window |

### Document Statuses

| Status | Meaning |
|---|---|
| **Valid** | Passed local validation |
| **Invalid** | Failed local validation |
| **Submitted** | Successfully sent to ETA, awaiting processing |
| **Rejected** | Rejected by ETA or receiver |
| **Cancelled** | Cancelled by the issuer |
| **Failed** | Submission to ETA failed |

---

## 10. Notifications

The **Notifications** form displays events from the ETA portal about your documents. The application periodically polls the ETA portal (configurable interval, default every 15 minutes) to check for status changes on your submitted documents.

### Filter

- **Event Type** — drop-down to filter by event type (All, Validation, Issuance, Rejection, Cancellation)
- **Unread Only** — checkbox to show only unread notifications

### Grid Columns

Timestamp, Event Type (color-coded), Internal ID, Message, Read/Unread.

### Color Coding

- **Red:** Rejection
- **Orange:** Cancellation
- **Green:** Issuance

### Actions

| Button | Description |
|---|---|
| **Refresh** | Fetches the latest notifications from the local database |
| **Mark All Read** | Marks all notifications as read |

Notification types: Validation, Issuance, Rejection, Cancellation.

### Desktop & Email Alerts

In addition to the in-app notification list, you can receive:

- **Windows desktop toast** — a balloon popup from the system tray icon
- **Email notification** — sent via SMTP to your configured address

Configure both in **Settings > Notifications** tab.

---

## 11. Settings

The **Settings** form has five tabs.

### Tab 1: Environment & Credentials

Two sub-tabs for **Preproduction** and **Production** environments. Each contains:

| Field | Description |
|---|---|
| **Client ID** | ETA portal client ID |
| **Client Secret** | ETA portal client secret |
| **API Base URL** | The ETA API endpoint URL |
| **Identity (Token) URL** | The ETA OAuth2 token endpoint |

### Tab 2: Digital Certificate (USB Token)

| Field / Action | Description |
|---|---|
| **Certificate selector** | Drop-down listing all available signing certificates from the Windows certificate store |
| **Refresh** | Re-scans the certificate store |
| **Certificate details** | Shows Subject, Issuer, Thumbprint, validity dates, and private key accessibility |
| **Test Sign** | Performs a test CADES-BES signature to verify the token is working and the PIN is accessible |

### Tab 3: Taxpayer Info

| Field | Description |
|---|---|
| **Registered Company / Taxpayer Name** | Your company name as registered with ETA |
| **Default Taxpayer Activity Code** | Your ETA activity code (ISIC code, required for document submission) |
| **Default Branch ID** | Branch identifier (default: 0) |
| **Issuer Governate** | Your company's governorate / region |
| **Issuer Region / City** | Your company's city |
| **Issuer Street** | Your company's street address |
| **Issuer Building Number** | Your company's building number |

### Tab 4: Notifications

| Section | Field | Description |
|---|---|---|
| **Event Filters** | Notify on Validation | Alert when ETA validates your document |
| | Notify on Issuance | Alert when ETA issues a UUID (document accepted) |
| | Notify on Rejection | Alert when document is rejected |
| | Notify on Cancellation | Alert when document is cancelled |
| | Check every (minutes) | How often to poll ETA for updates (1–1440, default 15) |
| **Desktop** | Show Windows desktop toast notifications | Pop a toast balloon on events |
| **Email** | Send email notifications | Forward events to email |
| | Notification email address | Recipient address |
| | SMTP Host | Mail server hostname |
| | Port | SMTP port (default 587) |
| | SMTP Username | Mail account username |
| | SMTP Password | Mail account password |
| | Use SSL / TLS | Encrypt the connection |

### Tab 5: Database

| Option | Description |
|---|---|
| **SQL Server** | Connect to a remote SQL Server instance |
| **LocalDB** | Auto-create database on `(localdb)\MSSQLLocalDB` — no additional configuration needed |

Fields for SQL Server mode:

| Field | Description |
|---|---|
| **Server IP / Host** | SQL Server address |
| **Port** | SQL Server port (default 1433) |
| **Username** | SQL Server login |
| **Password** | SQL Server password |
| **Test Connection** | Verifies the database connection and schema |
| **Status** | Shows connection test result |

---

## 12. Validation Rules

When you click **Validate** or **Sign & Submit**, the following rules are checked:

| Rule | Details |
|---|---|
| **Document Type** | Must be I, C, D, EI, EC, or ED |
| **Taxpayer Activity Code** | Must be set in Settings (required by ETA) |
| **Issuer RIN** | Must exist in current settings |
| **Issuer Governate** | Must be set in Settings |
| **Receiver Type** | Must be B (Business), P (Person), or F (Foreigner) |
| **Receiver RIN** | For B2B (Type B): must be exactly 9 digits |
| **Receiver Name** | Required for B2B |
| **Issue Date** | Required; cannot be more than 10 minutes in the future |
| **Line Items** | At least one line item required |
| **Line Description** | Required |
| **Line Item Code** | Required |
| **Item Type** | Must be GS1, EGS, or GPC |
| **Quantity** | Must be greater than zero |
| **Unit Price** | Must be greater than or equal to zero |
| **Tax Items** | At least one tax item per line |
| **Tax Type** | Must be T1 through T20 |
| **Tax Rate** | Must be between 0 and 100 |
| **Net Amount** | Must match the sum of line totals (within 0.05 tolerance) |
| **Total Amount** | Must be greater than zero |
| **Payment Method** | Must be C, V, M, O, or B |
| **Credit / Debit Note Reference** | UUID required for C, D, EC, and ED types |

**Payment codes:** C=Cash, V=Visa/Card, M=Mobile Wallet, O=Other, B=Bank Transfer

**Tax types:** T1=VAT (14%), T2=Table Tax, T3=Stamp Tax, T4=Entertainment Tax, T5=Resource Recovery, T6=Export Service Fee, T7=Health Education, T8–T20=Other

---

---

## 13. Keyboard Shortcuts

The application does not currently define any global or form-specific keyboard shortcuts. All actions are performed via mouse clicks on buttons and controls.

---

## 14. Troubleshooting & FAQ

### Login Issues

| Problem | Likely Cause | Solution |
|---|---|---|
| **"Client ID is required"** | Empty field | Enter your client ID |
| **"Taxpayer RIN must be 9 digits"** | Invalid RIN format | Enter exactly 9 digits |
| **"Login failed"** | Wrong credentials or network | Verify Client ID, Secret, RIN, and internet connection |
| **"Failed to connect to server"** | Network issue or wrong URLs | Check your internet and the API URLs in Settings |

### Document Creation Issues

| Problem | Likely Cause | Solution |
|---|---|---|
| **Validation fails on line items** | Missing or invalid fields | Check each line: description, code, qty > 0, valid tax type |
| **"Reference UUID is required"** | Credit/Debit note without reference | Enter the original invoice's UUID |
| **"Date issued cannot be in the future"** | Clock skew | Ensure your system clock is correct (10-minute buffer allowed) |
| **"Taxpayer activity code is required"** | Not configured in Settings | Go to Settings > Taxpayer Info and enter your activity code |
| **"Issuer governate is required"** | Not configured in Settings | Go to Settings > Taxpayer Info and enter your governate |

### Signing & Submission Issues

| Problem | Likely Cause | Solution |
|---|---|---|
| **"No signing certificates found"** | USB token not inserted or driver missing | Insert your token, ensure drivers are installed, click Refresh |
| **"Signing failed"** | PIN not entered or token disconnected | Re-insert token and try again |
| **"Submission failed"** | ETA API error | Check the error message. It may indicate an invalid RIN, duplicate submission, or schema issue |

### Database Issues

| Problem | Likely Cause | Solution |
|---|---|---|
| **"Failed to connect to database"** | Wrong server / port / credentials | Go to Settings > Database, verify connection info, click Test Connection |
| **LocalDB not available** | SQL Server Express LocalDB not installed | Install SQL Server 2019+ Express with LocalDB feature, or switch to SQL Server mode |

### General FAQ

| Question | Answer |
|---|---|
| **Where are my credentials stored?** | In the Windows registry, encrypted via DPAPI if "Save Credentials" was checked |
| **How do I switch environments?** | On the Login screen or in Settings > Environment & Credentials |
| **Can I use the app without a USB token?** | No — a signing certificate with private key access is required for ETA submissions (CADES-BES signature) |
| **What happens if I close the app during submission?** | The document may be left in a pending state. Use Search to check its status and re-submit if needed |
| **How do I get a CSV template?** | Click "Download Template" on the Batch Import form |
| **What if the ETA portal item codes endpoint doesn't work?** | Some environments don't expose this endpoint. You can maintain codes manually in the EGS Code Library |
| **How do I create an export invoice?** | Select "ei - Export Invoice" from the Document Type drop-down, set receiver country to a non-EG value, choose the foreign currency, and enter the exchange rate |
