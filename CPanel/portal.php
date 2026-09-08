<?php require 'config.php';

if ($_SERVER['REQUEST_METHOD'] === 'POST') {
    $user = $_POST['username'] ?? '';
    $pass = $_POST['password'] ?? '';
    if ($user === ADMIN_USER && password_verify($pass, ADMIN_PASS_HASH)) {
        $_SESSION['admin_logged_in'] = true;
        header('Location: manage.php');
        exit;
    }
    $error = 'Invalid credentials.';
}
?>
<!DOCTYPE html>
<html lang="en">
<head><meta charset="utf-8"><title>License Admin - Login</title>
<style>body{font-family:sans-serif;max-width:400px;margin:100px auto;padding:20px}
input{display:block;width:100%;margin:10px 0;padding:8px;box-sizing:border-box}
button{padding:10px 20px}</style></head>
<body>
<h2>License Admin Login</h2>
<?php if (isset($error)) echo "<p style='color:red'>$error</p>"; ?>
<form method="post">
    <input name="username" placeholder="Username" required>
    <input name="password" type="password" placeholder="Password" required>
    <button type="submit">Login</button>
</form>
</body>
</html>