const baseUrl = "https://localhost:44382/api/Users";

async function new_user() {
    const email = document.querySelector(".userName").value;
    const password = document.querySelector(".password").value;
    const firstName = document.querySelector(".firstName").value;
    const lastName = document.querySelector(".lastName").value;

    if (!email || !password || !firstName || !lastName) {
        alert("אנא מלא את כל השדות");
        return;
    }

    const postData = {
        Email: email,
        Password: password,
        FirstName: firstName,
        LastName: lastName,
        Phone: "",
        Address: "",
        Role: "User"
    };

    const response = await fetch(`${baseUrl}/register`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(postData)
    });

    if (response.ok) {
        alert("נרשמת בהצלחה!");
    } else {
        const error = await response.text();
        alert("הרישום נכשל: " + error);
    }
}

async function login() {
    const email = document.querySelector(".us").value; 
    const password = document.querySelector(".pas").value;

    const log = { email, password };

    const response = await fetch(`${baseUrl}/login`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(log)
    });

    if (response.ok) {
        const data = await response.json();
        sessionStorage.setItem('user', JSON.stringify(data));
        alert(`ברוך שובך, ${data.firstName || data.email}!`);
        window.location.href = "update.html";
    } else {
        alert("שם המשתמש או הסיסמה שגויים!");
    }
}
async function update() {
    const user = JSON.parse(sessionStorage.getItem('user'));
    if (!user) {
        alert("לא נמצא משתמש מחובר");
        return;
    }

    const email = document.querySelector("#userName").value;
    const firstName = document.querySelector("#firstName").value;
    const lastName = document.querySelector("#lastName").value;
    const password = document.querySelector("#password").value;

    const data = {
        Id: user.id,
        Email: email,
        FirstName: firstName,
        LastName: lastName,
        Password: password,
        Phone: user.phone || "",
        Address: user.address || ""
    };

    const response = await fetch(`${baseUrl}/${user.id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(data)
    });

    if (response.ok) {
        alert("הפרטים עודכנו בהצלחה");

        sessionStorage.setItem('user', JSON.stringify(data));
    } else {
        const errorText = await response.text();
        console.error("Update failed:", errorText);
        alert("עדכון נכשל: " + errorText);
    }
}


async function check_password() {
    const pass = document.querySelector(".password").value;

    
    const response = await fetch("https://localhost:44382/api/Passwords", {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(pass)
    });

    if (response.ok) {
        const dataPost = await response.json();
        const prog = document.querySelector(".progress");
        if (prog) prog.value = dataPost.strength * 25;
        return dataPost.strength / 4;
    }
    return 0;
}