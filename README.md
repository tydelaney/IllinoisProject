# GitHub_Conventions
This repository serves as a comprehensive guide to understanding and implementing GitHub conventions for the organization.

![pullrequest](https://github.com/American-Outdoor-Brands-Inc/GitHub_Conventions/assets/34846809/7b5ba860-e4ca-4da4-89eb-db743c380356)

# Production Requirements

For any production code or currently released code we want to follow the below process for new releases or changes.

```markdown
1. Create a new branch for anything you are working on in the format of {name}/{type}/{description}.
    a. Example 1 BugFix:
        i. Ty(your name)/bugfix/preventLoginCrash#88
    b. Example 2 Feature:
        i. Ty(your name)/feature/tournamentAdminConsole
2. Once the branch has been completed you want to create a pull request – this will allow us to perform a code review.
3. Once all of the approvals have been made we will merge the changes into the master/main branch and deploy the changes.
```

## Common Issues

1. You forgot to create a new branch before making changes on the local main/master branch.

    ### Solution
    If you forgot to create a new branch and made changes directly on `master/main` locally, follow these steps:

    1. Commit your changes on the `master/main` branch if you haven't already:
        ```bash
        git commit -a -m "Committing all changes before moving to a new branch because I forgot to make a new branch"
        ```

    2. Create and switch to a new branch. This will ensure the changes you made on `master` are present in this new branch.

        ```bash
        git checkout -b new-branch-name
        ```

    3. Now that the changes have been copied to the new branch, switch back to `master`.

        ```bash
        git checkout master
        ```

    4. To remove the last commit from `master`, you can perform a soft reset to keep the changes in the working directory.

        - Soft Reset (keeps changes in your working directory):
            ```bash
            git reset --soft HEAD~1
            ```
        - Hard Reset (discards changes, be careful):
            ```bash
            git reset --hard HEAD~1
            ```
        - Revert Commit (if you've already pushed to a remote repository):
            ```bash
            git revert HEAD
            ```

    5.  You can now switch back to your new branch and continue working:
        ```bash
        git checkout new-branch-name
        ```


### Creating a Pull Request with the New Branch

After your changes have been moved to a new branch and you've reverted `master` to its original state, you can proceed to create a pull request.

1. First, make sure you're on your new branch and it has the latest changes:
    ```bash
    git checkout new-branch-name
    git pull origin new-branch-name  # Ensure the branch is up-to-date
    ```

2. Push the branch to the remote repository if you haven't already:
    ```bash
    git push -u origin new-branch-name
    ```

3. Open GitHub and navigate to your repository. You should see a "Compare & pull request" button appear for your new branch. Click it to start creating the pull request.

4. Give your pull request a title and a comment explaining the changes. Make sure to tag any related issues or team members.

5. Choose the `master` (or `main`) branch as the base branch, and your new branch as the compare branch.

6. If everything looks good, click "Create pull request".

7. Wait for code reviews and resolve any requested changes or conflicts. After receiving approval, merge your pull request into `master`.

8. Finally, pull the updated `master` branch locally:
    ```bash
    git checkout master
    git pull origin master
    ```

9. Optionally, delete the local and remote copies of your feature branch:
    ```bash
    git branch -d new-branch-name              # Delete local branch
    git push origin --delete new-branch-name  # Delete remote branch
    ```

By following these steps, you'll create a pull request for your changes, get them reviewed, and merge them into `master`.


TO DO LIST
1. Merge all the code 
2. Add Password reset
3. Get rid of add account
4. profile picture -- DONE
5. Add email field in edit account -- DONE 
7. Transfer Net users data to account -fixed
8. Fix net users email inserting as username -stays
9. DK add all the comment stuff -in progress
10. Add ability to publish draft blog -- DONE
11. Need to user userManger or Account to get the account of the person logged in into the allblogposts
so I can see myblogpost because it doesn't know who is logged in. --DONE