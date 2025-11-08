const baseUrl = "https://localhost:44382/api/Users"; // שנה אם ה-API רץ על פורט אחר

async function new_user() {
    const userName = document.querySelector(".userName").value;
    const password = document.querySelector(".password").value;
    const firstName = document.querySelector(".firstName").value;
    const lastName = document.querySelector(".lastName").value;

    if (!userName || !password || !firstName || !lastName) {
        alert("אנא מלא את כל השדות");
        return;
    }

   

    if (password.length < 5) {
        alert("הסיסמה חייבת להיות לפחות 5 תווים");
        return;
    }

    const postData = { userName, password, firstName, lastName };

    const response = await fetch(baseUrl, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(postData)
    });

    if (response.ok) {
        alert("נרשמת בהצלחה!");
    } else {
        alert("הרישום נכשל!");
    }
}

async function login() {
    const userName = document.querySelector(".us").value;
    const password = document.querySelector(".pas").value;

    const log = { userName, password, firstName: "", lastName: "" };

    const response = await fetch(`${baseUrl}/Login`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(log)
    });

    if (response.ok) {
        const data = await response.json();
        sessionStorage.setItem('user', JSON.stringify(data));
        alert(`ברוך שובך, ${data.firstName || data.userName}!`);
    } else {
        alert("שם המשתמש או הסיסמה שגויים!");
    }
}

async function up_date() {
    const user = JSON.parse(sessionStorage.getItem('user'));
    if (!user) {
        alert("לא נמצא משתמש מחובר");
        return;
    }

    const userName = document.querySelector("#userName").value;
    const firstName = document.querySelector("#firstName").value;
    const lastName = document.querySelector("#lastName").value;
    const password = document.querySelector("#password").value;

    const data = { id: user.id, userName, firstName, lastName, password };

    const response = await fetch(`${baseUrl}/${user.id}`, {
        method: 'PUT',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(data)
    });

    if (response.ok) {
        sessionStorage.setItem('user', JSON.stringify(data));
        alert("הפרטים עודכנו בהצלחה");
    } else {
        alert("עדכון נכשל");
    }
}
