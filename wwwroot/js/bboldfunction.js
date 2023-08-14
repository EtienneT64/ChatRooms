<script>
    document.addEventListener("DOMContentLoaded", function () {
        const messageContent = document.getElementById("messageContent");
        const bold = document.getElementById("bold");

    bold.addEventListener("click", function () {
            const start = messageContent.selectionStart;
    const end = messageContent.selectionEnd;

    const selectedText = messageContent.value.substring(start, end);
    const modifiedText = `<b>${selectedText}</b>`;

    messageContent.setRangeText(modifiedText, start, end, "select");
        });
    });
</script>