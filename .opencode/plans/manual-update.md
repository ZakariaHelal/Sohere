# Update USER_MANUAL.md - Section 7 & 12

## Changes to Section 7 (Batch Import)

### 1. Update Preview Grid Columns
Change from:
```
Row, Internal ID, Type, Receiver, Total, Status, Errors.
```
To:
```
Row, Internal ID, Type, Receiver, Issuer, Total, Status, Errors.
```

### 2. Add "Data Structure" subsection (after "How to Use")

Insert this block:

```
### Data Structure

The import file uses a flat-row layout: each row represents one line item of an invoice.
Consecutive rows with the same InternalId are grouped into a single document.
The 26 columns fall into two categories:

| Category | Columns | Maps to ETA Structure |
|---|---|---|
| Header fields | 1-13 (InternalId through ReferenceUuid) | Core document |
| Line item fields | 14-26 (ItemDescription through CurrencyExchangeRate) | invoiceLines[] |

Grouping rule: All rows sharing the same InternalId produce one document.
Header values on the last row for a given InternalId override earlier ones.
```

### 3. Add "Computed & Derived Amounts" subsection

Insert this block after Data Structure:

```
### Computed & Derived Amounts

The following amounts are calculated by the application:

| Field | Formula |
|---|---|
| Sales Total | Quantity x UnitPrice (per line) |
| Net Total | SalesTotal - DiscountAmount (per line) |
| Tax Amount | NetTotal x (TaxRate / 100) (per line) |
| Total | NetTotal + TaxAmount (per line) |
| Document Total | Sum of all line Totals |
```

## Changes to Section 12 (CSV / Excel Import Format)

### 1. Expand field descriptions in the 26-column table

Each Description cell needs more detail referencing ETA SDK structures.
Key enhancements:
- InternalId: mention it is the grouping key
- ItemType/UnitType: reference ETA invoiceLine fields
- UnitPrice: mention it maps to unitValue/amountEGP (or amountSold for FC)
- DiscountAmount: mention it maps to invoiceLine/discount/amount
- TaxType/TaxSubType/TaxRate: reference invoiceLine/taxableItems[]
- CurrencySold/AmountSold/CurrencyExchangeRate: reference invoiceLine/unitValue sub-structure
- DateTimeIssued: mention ISO 8601 format expected

### 2. Add "Computed Amounts Reference" table

Add a table showing how derived fields map to ETA:
- SalesTotal -> invoiceLine/salesTotal
- NetTotal -> invoiceLine/netTotal
- TaxAmount -> invoiceLine/taxableItems[].amount
- Total -> invoiceLine/total
- document-level totals: totalSalesAmount, netAmount, totalAmount

### 3. Add "ETA Sub-Structure Reference"

Show which import columns map to ETA sub-structures:
- Value (unitValue): UnitPrice, CurrencySold, AmountSold, CurrencyExchangeRate
- Discount (discount): DiscountAmount
- TaxableItem (taxableItems[]): TaxType, TaxSubType, TaxRate

### 4. Also update Section 8 (Search)

Add "Issuer Name" text filter to the filter table and "Issuer" column to Results Grid columns.
