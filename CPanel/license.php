<?php
// POST /api/v1/license.php — License validation endpoint
header('Content-Type: application/json; charset=utf-8');

if ($_SERVER['REQUEST_METHOD'] !== 'POST') {
    http_response_code(405);
    echo json_encode(['isValid'=>false,'message'=>'Method not allowed.']);
    exit;
}

// Share credentials with admin/config.php
require_once __DIR__ . '/../../admin/config.php';

try {
    $pdo = new PDO("mysql:host=".DB_HOST.";dbname=".DB_NAME.";charset=utf8mb4", DB_USER, DB_PASS, [
        PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION,
        PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_ASSOC,
    ]);
} catch (PDOException $e) {
    http_response_code(500);
    echo json_encode(['isValid'=>false,'message'=>'Server error.']);
    exit;
}

// Read form-encoded input
$licenseKey = trim($_POST['license_key'] ?? '');
$hwid       = trim($_POST['hwid'] ?? '');
$appVersion = trim($_POST['app_version'] ?? '');

if ($licenseKey === '' || $hwid === '') {
    http_response_code(400);
    echo json_encode(['isValid'=>false,'message'=>'Missing required fields.']);
    exit;
}

// Look up license
$stmt = $pdo->prepare('SELECT * FROM licenses WHERE license_key = ? LIMIT 1');
$stmt->execute([$licenseKey]);
$license = $stmt->fetch();

if (!$license) {
    echo json_encode(['isValid'=>false,'message'=>'Invalid license key.']);
    exit;
}

if (!$license['is_active']) {
    echo json_encode(['isValid'=>false,'message'=>'License has been revoked.']);
    exit;
}

$expiry = $license['expiry_date'];
if (strtotime($expiry) < time()) {
    echo json_encode(['isValid'=>false,'message'=>'License expired on '.$expiry.'.']);
    exit;
}

// HWID binding
if ($license['hwid'] === null) {
    $update = $pdo->prepare('UPDATE licenses SET hwid = ?, activation_count = activation_count + 1, last_check_utc = UTC_TIMESTAMP() WHERE id = ?');
    $update->execute([$hwid, $license['id']]);
} elseif ($license['hwid'] !== $hwid) {
    echo json_encode(['isValid'=>false,'message'=>'License already activated on another machine.']);
    exit;
} else {
    $update = $pdo->prepare('UPDATE licenses SET last_check_utc = UTC_TIMESTAMP() WHERE id = ?');
    $update->execute([$license['id']]);
}

echo json_encode([
    'isValid'      => true,
    'expiryDate'   => $expiry,
    'customerName' => $license['customer_name'],
    'plan'         => $license['plan'],
    'message'      => 'License valid until '.$expiry.'.',
]);