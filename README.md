# Sphere ERP (ETA e-Invoicing Desktop App)

A Windows desktop application for submitting invoices, credit notes, debit notes, and export invoices to the Egyptian Tax Authority (ETA) e-Invoicing portal, signing documents with a USB token/HSM digital certificate, and managing the full document lifecycle (search, cancel, reject, print/PDF, notifications).

Built with VB.NET / .NET 8 WinForms.

## 1. Prerequisites

- **Visual Studio 2022** (17.8 or later) with the **.NET desktop development** workload installed.
- **.NET 8 SDK**.
- **SQL Server Express** or **SQL Server LocalDB**.
- Your ETA-issued **USB token or HSM** with its PKCS#11 minidriver installed.
- Your ETA **Client ID / Client Secret** for Preproduction and Production environments.

## 2. Opening the project

1. Open `SphereERP.sln` in Visual Studio.
2. Let NuGet restore the packages.
3. Build the solution (Ctrl+Shift+B). It targets `net8.0-windows`.

## 3. Database setup

Run `SQL/01_CreateDatabase.sql` on your SQL Server instance. This creates the `ETAInvoicingDB` database and all required tables. Update the connection string in `App.config` or from inside the app via **Settings → Database**.

## 4. USB Token / HSM setup

1. Install the PKCS#11 minidriver from your token vendor.
2. Confirm your signing certificate appears under **Certificates – Personal** in `certmgr.msc`.
3. In the app, go to **Settings → Digital Certificate (USB Token)**, click **Refresh**, select your certificate, and click **Test Sign**.

## 5. Configuring credentials

Launch the app, choose **Preproduction** or **Production**, enter your Client ID, Client Secret, and Taxpayer RIN. Use the Settings page to configure your taxpayer info, activity code, and notification preferences.

## 6. Feature map

| Feature | Where to find it |
|---|---|
| Login & token management | Login screen |
| Dashboard | Main navigation → Dashboard |
| Submit any document type | Main navigation → New Document |
| Submit from CSV/Excel | Main navigation → Batch Import |
| Search & manage documents | Main navigation → Search |
| Cancel / Reject documents | Search → select a row → Cancel/Reject |
| PDF printout | Available after submission and from Search |
| EGS item codes | Main navigation → EGS Codes |
| Notifications | Main navigation → Notifications |
| USB token / HSM signing | Settings → Digital Certificate (USB Token) |

## 7. Important — API endpoints

The ETA API paths are in `Services/EtaApiClient.vb`. Verify them against the current ETA SDK reference before going live:

| Purpose | Path |
|---|---|
| OAuth2 token | `{IdentityUrl}/connect/token` |
| Submit documents | `POST /api/v1.0/documentsubmissions` |
| Document details | `GET /api/v1.0/documents/{uuid}/details` |
| Search documents | `GET /api/v1.0/documents/search` |
| Cancel/Reject | `PUT /api/v1.0/documents/{uuid}/state` |
| Download PDF | `GET /api/v1.0/documents/{uuid}/pdf` |
| Registered item codes | `GET /api/v1.0/itemcodes` |
