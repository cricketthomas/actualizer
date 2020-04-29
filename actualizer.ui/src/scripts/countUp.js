var countUp = function (finalVal, ref) {
    let i = 0;
    setInterval(() => {
        if (i < finalVal) {
            this.$refs[ref].innerHTML = ++ i;
        } else {
            clearInterval(finalVal);
        }
    }, 1000/ finalVal);
};

export default countUp;