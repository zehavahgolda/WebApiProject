const baseUrl = "https://localhost:44382/api/Users";


//משתמש חדש
async function new_user() {
    const userName = document.querySelector(".userName").value;
    const password = document.querySelector(".password").value;
    const firstName = document.querySelector(".firstName").value;
    const lastName = document.querySelector(".lastName").value;

    if (!userName || !password || !firstName || !lastName) {
        alert("אנא מלא את כל השדות");
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

//כניסת משתמש רשום
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
           window.location.href = "update.html"; 
    } else {
        alert("שם המשתמש או הסיסמה שגויים!");
    }
}


//עדכון משתמש
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


//בדיקת חוזק סיסמא
async function check_password() {
    const pass = document.querySelector(".password").value;
        const response = await fetch("https://localhost:44382/api/Passwords", {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            },
            body: JSON.stringify(pass)

        });
        const dataPost = await response.json();
        const prog = document.querySelector(".progress");
        prog.value = dataPost.strength * 25;
        console.log(dataPost);
        if (response.status == 200) {
            return dataPost.strength / 4;
        }
        else {
            return 0;
        }
    }

