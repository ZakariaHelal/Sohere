<?php
require 'config.php';
if (!isset($_SESSION['admin_logged_in']) || !$_SESSION['admin_logged_in']) {
    header('Location: portal.php'); exit;
}

try {
    $pdo = new PDO("mysql:host=".DB_HOST.";dbname=".DB_NAME.";charset=utf8mb4", DB_USER, DB_PASS, [
        PDO::ATTR_ERRMODE => PDO::ERRMODE_EXCEPTION,
        PDO::ATTR_DEFAULT_FETCH_MODE => PDO::FETCH_ASSOC,
    ]);
} catch (PDOException $e) {
    die('Database connection failed.');
}

// Generate a new license
if (isset($_POST['generate'])) {
    $key = 'ETA-' . strtoupper(bin2hex(random_bytes(6)) . '-' . bin2hex(random_bytes(3)));
    $stmt = $pdo->prepare('INSERT INTO licenses (license_key, customer_name, plan, expiry_date) VALUES (?,?,?,?)');
    $stmt->execute([$key, $_POST['customer_name'], $_POST['plan'], $_POST['expiry_date']]);
    $msg = "License $key created.";
}

// Toggle active status
if (isset($_GET['toggle'])) {
    $stmt = $pdo->prepare('UPDATE licenses SET is_active = NOT is_active WHERE id = ?');
    $stmt->execute([(int)$_GET['toggle']]);
    header('Location: manage.php'); exit;
}

$licenses = $pdo->query('SELECT * FROM licenses ORDER BY created_at DESC')->fetchAll();

if (isset($_GET['logout'])) { session_destroy(); header('Location: portal.php'); exit; }
?>
<!DOCTYPE html>
<html lang="en">
<head><meta charset="utf-8"><title>License Admin</title>
<style>body{font-family:sans-serif;max-width:1000px;margin:20px auto;padding:20px}
table{width:100%;border-collapse:collapse;margin-top:20px}
th,td{border:1px solid #ccc;padding:8px;text-align:left}
th{background:#f5f5f5}
input,select,button{padding:6px;margin:4px}
a{color:#c00;text-decoration:none}
.msg{background:#dfd;padding:10px;border-radius:4px}</style></head>
<body>
<h2>License Management</h2>
<p><a href="?logout=1" style="float:right">Logout</a></p>
<?php if (isset($msg)) echo "<div class='msg'>$msg</div>"; ?>

<h3>Generate New License</h3>
<form method="post">
    <input name="customer_name" placeholder="Customer Name" required>
    <select name="plan">
        <option>Basic</option>
        <option>Professional</option>
        <option selected>Enterprise</option>
    </select>
    <input name="expiry_date" type="date" required>
    <button name="generate">Generate</button>
</form>

<h3>All Licenses</h3>
<table>
<tr><th>ID</th><th>License Key</th><th>Customer</th><th>Plan</th><th>Expiry</th><th>HWID</th><th>Active</th><th>Last Check</th><th>Action</th></tr>
<?php foreach ($licenses as $lic): ?>
<tr>
    <td><?= $lic['id'] ?></td>
    <td><code><?= htmlspecialchars($lic['license_key']) ?></code></td>
    <td><?= htmlspecialchars($lic['customer_name']) ?></td>
    <td><?= $lic['plan'] ?></td>
    <td><?= $lic['expiry_date'] ?></td>
    <td><code><?= $lic['hwid'] ? htmlspecialchars(substr($lic['hwid'],0,16)).'&hellip;' : '&mdash;' ?></code></td>
    <td><?= $lic['is_active'] ? '&#9989;' : '&#10060;' ?></td>
    <td><?= $lic['last_check_utc'] ?? '&mdash;' ?></td>
    <td><a href="?toggle=<?= $lic['id'] ?>" onclick="return confirm('Toggle?')">
        <?= $lic['is_active'] ? 'Revoke' : 'Activate' ?></a></td>
</tr>
<?php endforeach; ?>
</table>
</body>
</html>