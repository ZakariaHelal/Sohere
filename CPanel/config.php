<?php
// Database — update these with your cPanel DB details
define('DB_HOST', 'localhost');
define('DB_NAME', 'alhayateg_ETAInvoicing');
define('DB_USER', 'alhayateg_InvoicingAPI');
define('DB_PASS', 'Cairo@2020');

// Admin login
define('ADMIN_USER', 'admin');
// Generate a bcrypt hash at https://bcrypt-generator.com/ and paste it here
define('ADMIN_PASS_HASH', '$2a$12$G7WjvljbfCTWDCawSTkdzeIMiJXdVeDu1R8sTrZlZaxN8Msekh0dS');

session_start();