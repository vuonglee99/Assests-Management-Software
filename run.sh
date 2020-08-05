# 1. Set traps to clean up background jobs when script being terminated:
# - Start and stop bash background process (set trap even before running jobs): https://spin.atomicobject.com/2017/08/24/start-stop-bash-background-process/
### How to run a command when received INT (Ctrl + C) signal: https://stackoverflow.com/questions/4118165/bash-how-to-execute-a-command-on-script-termination
### What's the difference between 'kill' and 'pkill' command: https://www.linuxhelp.com/questions/difference-between-kill-and-pkill-command
### How to run 2 commands in one line: https://www.howtogeek.com/269509/how-to-run-two-or-more-terminal-commands-at-once-in-linux
trap 'exit || echo "cleaning up failed"' INT TERM ERR
trap 'kill 0 || echo "cleaning up failed"' EXIT


# 2. How to start running commands (jobs) simultaneously on background: https://www.slashroot.in/how-run-multiple-commands-parallel-linux

cd ./aspnet-core/src/GSoft.AbpZeroTemplate.Web.Host/ 
dotnet run & # & is for parallel, && is for sequential: https://stackoverflow.com/a/39172660/9787887

cd ../../../angular/

# Check if a directory exists: https://www.cyberciti.biz/faq/howto-check-if-a-directory-exists-in-a-bash-shellscript
if [ -d 'node_modules' ]
then
  npm start &
else
  npm install && npm start & # && is for sequential, & is for parallel (background) jobs 
fi

wait