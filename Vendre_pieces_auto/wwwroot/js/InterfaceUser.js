/* Set the width of the side navigation to 250px */
function openNav() {
    document.getElementById("mySidenav").style.width = "250px";
  }
  
  /* Set the width of the side navigation to 0 */
  function closeNav() {
    document.getElementById("mySidenav").style.width = "0";
  }
  function afficherpop(){
    var pop=document.querySelector(".popbackground");
    pop.classList.add("active");
    pop.addEventListener("click",(event)=>{
        if(event.target === pop){
            pop.classList.remove("active");
        }
    })
}
