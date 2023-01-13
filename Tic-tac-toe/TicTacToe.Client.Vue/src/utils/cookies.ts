function setCookie(name: string, value: string, days: number) {
    let expires = "";
    if (days) {
        const date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toUTCString();
    }
    document.cookie = name + "=" + (value || "") + expires + "; path=/";
}


const getCookie = (name: string) => {
    return document.cookie
        .split('; ')
        .find((row) => row.startsWith(name))
        ?.split('=')[1]
}
export {
    getCookie,
    setCookie,
}
